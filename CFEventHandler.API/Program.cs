using CFEventHandler.API.Extensions;
using CFEventHandler.API.HealthCheck;
using CFEventHandler.API.Hubs;
using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Models;
using CFEventHandler.API.Security;
using CFEventHandler.API.Services;
using CFEventHandler.API.Utilities;
using CFEventHandler.Common.Services;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Process;
using CFEventHandler.Services;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.SystemTasks;
using CFEventHandler.Teams;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

// For tests:
// 1) Add InternalsVisibleTo line.
// 2) Add "partial" declaration for Program class so that it's visible to test project
[assembly: InternalsVisibleTo("CFEventHandler.xUnit")]     // Allows test project to see this
//var isTestsRunning = ConfigUtilities.IsTestsRunning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger config. Allow user to entry API key
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your API Key",
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "APIKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpContextAccessor();  // For IRequestInfoService
builder.Services.AddSignalR();

// CMF Added
builder.Services.AddAuthentication().AddScheme<ApiKeyAuthOptions, ApiKeyAuthHandler>("APIKey", (options) =>
{
    //builder.Configuration.GetRequiredSection("ApiKeyOptions").Bind(options);
});
builder.Services.AddAuthorization();    // CMF Added (For API key)

// Set health checks
builder.Services.AddHealthChecks()
    .AddCheck<DataHealthCheck>("Data");

// Add fluent validation 
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Configure database config from appSettings.json
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection(nameof(DatabaseConfig)));
builder.Services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

// Set cache for API keys
builder.Services.AddSingleton<IAPIKeyCacheService, APIKeyCacheService>();

// Document template processor
builder.Services.AddScoped<IDocumentTemplateProcessor, DocumentTemplateProcessor>();

// Security admin
builder.Services.AddScoped<ISecurityAdmin, SecurityAdmin>();

// Database admin
builder.Services.AddScoped<IDatabaseAdmin, MongoDBAdmin>();

// Event settings service (JSON for the moment)
builder.Services.AddScoped<IConsoleSettingsService, MongoDBConsoleSettingsService>();
builder.Services.AddScoped<ICSVSettingsService, MongoDBCSVSettingsService>();
//builder.Services.AddScoped<ICustomSettingsService>((scope) =>
//{
//    return new JSONCustomSettingsService(Path.Combine(dataFolder, "CustomSettings"));
//});
builder.Services.AddScoped<IEmailSettingsService, MongoDBEmailSettingsService>();
builder.Services.AddScoped<IHTTPSettingsService, MongoDBHTTPSettingsService>();
builder.Services.AddScoped<IProcessSettingsService, MongoDBProcessSettingsService>();
builder.Services.AddScoped<ISignalRSettingsService, MongoDBSignalRSettingsService>();
builder.Services.AddScoped<ISMSSettingsService, MongoDBSMSSettingsService>();
builder.Services.AddScoped<ISQLSettingsService, MongoDBSQLSettingsService>();
builder.Services.AddScoped<ITeamsSettingsService, MongoDBTeamsSettingsService>();

// General data services
builder.Services.AddScoped<IAPIKeyService, MongoDBAPIKeyService>();
builder.Services.AddScoped<IDocumentTemplateService, MongoDBDocumentTemplateService>();
builder.Services.AddScoped<IEventClientService, MongoDBEventClientService>();
builder.Services.AddScoped<IEventHandlerRuleService, MongoDBEventHandlerRuleService>();
builder.Services.AddScoped<IEventHandlerService, MongoDBEventHandlerService>();
builder.Services.AddScoped<IEventService, MongoDBEventService>();
builder.Services.AddScoped<IEventTypeService, MongoDBEventTypeService>();

// Set event manager that handles events
builder.Services.AddScoped<IEventManagerService, EventManagerService>();

// Set request info service for getting request info
builder.Services.AddScoped<IRequestInfoService, RequestInfoService>();

// Set event queue
builder.Services.AddSingleton<IEventQueueService, MemoryEventQueueService>();

// Set system tasks list
builder.Services.AddSingleton<ISystemTasks>((scope) =>
{
    var systemTasks = new List<ISystemTask>()
    {
        /*
        new DeleteOldEventsTask(new SystemTaskSchedule() 
            { 
                ExecuteFrequency = TimeSpan.FromHours(12), 
                LastExecuteTime = DateTimeUtilities.GetStartOfDay(DateTimeOffset.UtcNow)
            })
        */
        /*
        new RandomEventsTask(new SystemTaskSchedule()
            {
                ExecuteFrequency = TimeSpan.FromSeconds(30),
                LastExecuteTime = DateTimeUtilities.GetStartOfDay(DateTimeOffset.UtcNow)
            })
        */
    };
    systemTasks.RemoveAll(st => st == null);
    return new SystemTasks(systemTasks);
});

// Set background service for processing events
builder.Services.AddHostedService<EventBackgroundService>();

// Add background processing
builder.Services.AddHostedService<SystemTaskBackgroundService>();

// Register all event handlers
builder.Services.RegisterAllTypes<IEventHandler>(new[] { typeof(IEventHandler).Assembly });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.MapHub<NotificationHub>("/notificationhub");

// Register middleware for unhandled exceptions
app.UseMiddleware<ErrorMiddlewareService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialise
using (var scope = app.Services.CreateScope())
{    
    // Initialise DB
    var databaseAdmin = scope.ServiceProvider.GetRequiredService<IDatabaseAdmin>();
    await databaseAdmin.InitialiseAsync();

    // Refresh API key cache
    var securityAdmin = scope.ServiceProvider.GetRequiredService<ISecurityAdmin>();
    securityAdmin.RefreshAPIKeyCache();
}

app.Run();

public partial class Program { }   // Only needed so that class visible for unit tests