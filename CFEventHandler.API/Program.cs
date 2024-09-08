using CFEventHandler.API.Extensions;
using CFEventHandler.API.HealthCheck;
using CFEventHandler.API.Hubs;
using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Services;
using CFEventHandler.Common.Interfaces;
using CFEventHandler.Common.Process;
using CFEventHandler.Console;
using CFEventHandler.Custom;
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

// TODO: Remove this
var dataFolder = @"D:\\Data\\Temp\\EventHandlerData";

// Event settings service (JSON for the moment)
builder.Services.AddScoped<IConsoleSettingsService>((scope) =>
{
    return new JSONConsoleSettingsService(Path.Combine(dataFolder, "ConsoleSettings"));
});
builder.Services.AddScoped<ICSVSettingsService>((scope) =>
{
    return new JSONCSVSettingsService(Path.Combine(dataFolder, "CSVSettings"));
});
builder.Services.AddScoped<ICustomSettingsService>((scope) =>
{
    return new JSONCustomSettingsService(Path.Combine(dataFolder, "CustomSettings"));
});
builder.Services.AddScoped<IEmailSettingsService>((scope) =>
{
    return new JSONEmailSettingsService(Path.Combine(dataFolder, "EmailSettings"));
});
builder.Services.AddScoped<IHTTPSettingsService>((scope) =>
{
    return new JSONHTTPSettingsService(Path.Combine(dataFolder, "HTTPSettings"));
});
builder.Services.AddScoped<IProcessSettingsService>((scope) =>
{
    return new JSONProcessSettingsService(Path.Combine(dataFolder, "ProcessSettings"));
});
builder.Services.AddScoped<ISignalRSettingsService>((scope) =>
{
    return new JSONSignalRSettingsService(Path.Combine(dataFolder, "SignalRSettings"));
});
builder.Services.AddScoped<ISMSSettingsService>((scope) =>
{
    return new JSONSMSSettingsService(Path.Combine(dataFolder, "SMSSettings"));
});
builder.Services.AddScoped<ISQLSettingsService>((scope) =>
{
    return new JSONSQLSettingsService(Path.Combine(dataFolder, "SQLSettings"));
});
builder.Services.AddScoped<ITeamsSettingsService>((scope) =>
{
    return new JSONTeamsSettingsService(Path.Combine(dataFolder, "TeamsSettings"));
});

// General data services
builder.Services.AddScoped<IEventClientService>((scope) =>
{
    return new JSONEventClientService(Path.Combine(dataFolder, "EventClients"));
});
builder.Services.AddScoped<IEventHandlerRuleService>((scope) =>
{
    return new JSONEventHandlerRuleService(Path.Combine(dataFolder, "EventHandlerRules"));
});
builder.Services.AddScoped<IEventHandlerService>((scope) =>
{
    return new JSONEventHandlerService(Path.Combine(dataFolder, "EventHandlers"));
});
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventTypeService>((scope) =>
{
    return new JSONEventTypeService(Path.Combine(dataFolder, "EventTypes"));
});

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

app.Run();
