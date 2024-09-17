using CFEventHandler.API.Extensions;
using CFEventHandler.API.HealthCheck;
using CFEventHandler.API.Hubs;
using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Services;
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
using CFEventHandler.Teams;
using FluentValidation;
using FluentValidation.AspNetCore;
using CFEventHandler.Common.Email;
using CFEventHandler.API.Models;
using Microsoft.Extensions.Options;
using CFEventHandler.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpContextAccessor();  // For IRequestInfoService
builder.Services.AddSignalR();

// Set health checks
builder.Services.AddHealthChecks()
    .AddCheck<DataHealthCheck>("Data");

// Add fluent validation 
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Configure database config from appSettings.json
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection(nameof(DatabaseConfig)));
builder.Services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

// TODO: Remove this
var dataFolder = @"D:\\Data\\Temp\\EventHandlerData";

// Document template processor
builder.Services.AddScoped<IDocumentTemplateProcessor, DocumentTemplateProcessor>();

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

// Set background service for processing events
builder.Services.AddHostedService<EventBackgroundService>();

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
//app.UseMiddleware<ErrorMiddlewareService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialise database
using (var scope = app.Services.CreateScope())
{    
    var databaseAdmin = scope.ServiceProvider.GetRequiredService<IDatabaseAdmin>();
    await databaseAdmin.InitialiseAsync();    
}

app.Run();
