using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Services;
using CFEventHandler.Common.Interfaces;
using CFEventHandler.Common.Process;
using CFEventHandler.Common.Services;
using CFEventHandler.Common.SignalR;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Custom;
using CFEventHandler.Email;
using CFEventHandler.HealthCheck;
using CFEventHandler.HTTP;
using CFEventHandler.Hubs;
using CFEventHandler.Interfaces;
using CFEventHandler.Process;
using CFEventHandler.Services;
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

// Event settings service
builder.Services.AddScoped<IConsoleSettingsService, ConsoleSettingsService>();
builder.Services.AddScoped<ICSVSettingsService, CSVSettingsService>();
//builder.Services.AddScoped<ICustomSettingsService, CustomSettingsService>();  // TODO: Consider removing
builder.Services.AddScoped<IEmailSettingsService, EmailSettingsService>();
builder.Services.AddScoped<IHTTPSettingsService, HTTPSettingsService>();
builder.Services.AddScoped<IProcessSettingsService, ProcessSettingsService>();
builder.Services.AddScoped<ISignalRSettingsService, SignalRSettingsService>();
builder.Services.AddScoped<ISQLSettingsService, SQLSettingsService>();
builder.Services.AddScoped<ITeamsSettingsService, TeamsSettingsService>();

// General data services
builder.Services.AddScoped<IEventClientService, EventClientService>();
builder.Services.AddScoped<IEventHandlerRuleService, EventHandlerRuleService>();
builder.Services.AddScoped<IEventHandlerService, EventHandlerService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();

// Set event manager that handles events
builder.Services.AddScoped<IEventManagerService, EventManagerService>();

// Set request info service for getting request info
builder.Services.AddScoped<IRequestInfoService, RequestInfoService>();

// Set event queue
builder.Services.AddSingleton<IEventQueueService, MemoryEventQueueService>();

// Set background service for processing events
builder.Services.AddHostedService<EventBackgroundService>();

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
