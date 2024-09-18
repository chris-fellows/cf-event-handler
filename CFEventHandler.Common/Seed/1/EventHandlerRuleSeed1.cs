using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.Enums;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;

namespace CFEventHandler.Seed
{
    public class EventHandlerRuleSeed1 : IEntityList<EventHandlerRule>
    {
        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISignalRSettingsService _signalRSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ISQLSettingsService _sqlSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;            

        public EventHandlerRuleSeed1(IConsoleSettingsService consoleSettingsService,
                            ICSVSettingsService csvSettingsService, 
                            IEmailSettingsService emailSettingsService,
                            IEventHandlerService eventHandlerService,
                            IEventTypeService eventTypeService,
                            IHTTPSettingsService httpSettingsService,
                            IProcessSettingsService processSettingsService,
                            ISignalRSettingsService signalRSettingsService,
                            ISMSSettingsService smsSettingsService,
                            ISQLSettingsService sqlSettingsService,
                            ITeamsSettingsService teamsSettingsService)
        {
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _emailSettingsService = emailSettingsService;
            _eventHandlerService = eventHandlerService;
            _eventTypeService = eventTypeService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
        }

        public async Task<List<EventHandlerRule>> ReadAllAsync()
        {
            var eventHandlerRules = new List<EventHandlerRule>();

            // Get event handlers
            var eventHandlers = _eventHandlerService.GetAll();
            var eventHandlerConsole = eventHandlers.First(eh => eh.Name == "Console");
            var eventHandlerEmail = eventHandlers.First(eh => eh.Name == "Email");

            // Get event types
            var eventTypes = _eventTypeService.GetAll();
            var eventTypeTest1 = eventTypes.First(et => et.Name == "Test event 1");
            var eventTypeTest2 = eventTypes.First(et => et.Name == "Test event 2");

            var emailSettingsList = _emailSettingsService.GetAll();
            var emailSettings1 = emailSettingsList.First(es => es.Name == "Email (Default)");
            var emailSettings2 = emailSettingsList.First(es => es.Name == "Email (2)");

            // Add email #1 handler
            eventHandlerRules.Add(new EventHandlerRule()
            {
                //Id = "1",
                EventTypeId = eventTypeTest1.Id,
                EventHandlerId = eventHandlerEmail.Id,
                EventSettingsId = emailSettings1.Id,
                Name = $"Event handler ({eventHandlerEmail.Name}:{eventTypeTest1.Name})",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        ItemName = "CompanyId",
                        ConditionType = ConditionTypes.Equals,
                        Values = new List<object>() { 1 }                        
                    }
                }
            });

            // Add email #2 handler
            eventHandlerRules.Add(new EventHandlerRule()
            {
                //Id = "2",
                EventTypeId = eventTypeTest2.Id,
                EventHandlerId = eventHandlerEmail.Id,
                EventSettingsId = emailSettings2.Id,
                Name = $"Event handler ({eventHandlerEmail.Name}:{eventTypeTest2.Name})",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        ItemName = "CompanyId",
                        ConditionType = ConditionTypes.Equals,
                        Values = new List<object>() { 2 }
                    }
                }
            });

            // Add console event handler for every event
            foreach(var eventType in eventTypes)
            {
                eventHandlerRules.Add(new EventHandlerRule()
                {
                    //Id = "2",
                    EventTypeId = eventType.Id,
                    EventHandlerId = eventHandlerConsole.Id,                    
                    Name = $"Event handler ({eventHandlerConsole.Name}:{eventType.Name})",
                    Conditions = new List<Condition>()
                    {
                        new Condition()
                        {
                            ItemName = "CompanyId",
                            ConditionType = ConditionTypes.Equals,
                            Values = new List<object>() { 1 }
                        }
                    }
                });
            }

            return eventHandlerRules;
        }

        public async Task WriteAllAsync(List<EventHandlerRule> eventHandlerRules)
        {
            // No action
        }
    }
}
