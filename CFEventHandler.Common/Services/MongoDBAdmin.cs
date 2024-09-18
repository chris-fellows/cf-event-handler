using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandlerObject = CFEventHandler.Models.EventHandler;
using MongoDB.Driver;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Process;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using CFEventHandler.Seed;

namespace CFEventHandler.Services
{
    public class MongoDBAdmin : IDatabaseAdmin
    {
        private readonly IDatabaseConfig _databaseConfig;
        private readonly IEventClientService _eventClientService;
        private readonly IEventHandlerRuleService _eventHandlerRuleService;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;        

        // Event settings
        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISignalRSettingsService _signalRSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ISQLSettingsService _sqlSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;

        public MongoDBAdmin(IDatabaseConfig databaseConfig, 
                        IEventClientService eventClientService,
                        IEventHandlerRuleService eventHandlerRuleService,
                        IEventHandlerService eventHandlerService,
                        IEventService eventService,
                        IEventTypeService eventTypeService,
                        IConsoleSettingsService consoleSettingsService,
                        ICSVSettingsService csvSettingsService,
                        IEmailSettingsService emailSettingsService,
                        IHTTPSettingsService httpSettingsService,
                        IProcessSettingsService processSettingsService,
                        ISignalRSettingsService signalRSettingsService,
                        ISMSSettingsService smsSettingsService,
                        ISQLSettingsService sqlSettingsService,
                        ITeamsSettingsService teamsSettingsService)
        {
            _databaseConfig = databaseConfig;
            _eventClientService = eventClientService;
            _eventHandlerRuleService = eventHandlerRuleService;
            _eventHandlerService = eventHandlerService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _emailSettingsService = emailSettingsService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
        }        

        public async Task InitialiseAsync()
        {
            var client = new MongoClient(_databaseConfig.ConnectionString);
            var database = client.GetDatabase(_databaseConfig.DatabaseName);

            // Initialise collections
            await InitialiseEventClients(database);
            await InitialiseEventTypes(database);
            await InitialiseEventHandlers(database);
            await InitialiseEventHandlerRules(database);
            await InitialiseEvents(database);
        }        

        private async Task InitialiseEventClients(IMongoDatabase database)
        {
            var collection = database.GetCollection<EventClient>("event_clients");
            var indexDefinitionName = Builders<EventClient>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<EventClient>(indexDefinitionName));
        }

        private async Task InitialiseEventTypes(IMongoDatabase database)
        {
            var collection = database.GetCollection<EventType>("event_types");
            var indexDefinitionName = Builders<EventType>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<EventType>(indexDefinitionName));
        }

        private async Task InitialiseEventHandlers(IMongoDatabase database)
        {
            var collection = database.GetCollection<CFEventHandlerObject>("event_handlers");
            var indexDefinitionName = Builders<CFEventHandlerObject>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<CFEventHandlerObject>(indexDefinitionName));
        }

        private async Task InitialiseEventHandlerRules(IMongoDatabase database)
        {
            var collection = database.GetCollection<EventHandlerRule>("event_handler_rules");
            var indexDefinitionName = Builders<EventHandlerRule>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<EventHandlerRule>(indexDefinitionName));
        }

        private async Task InitialiseEvents(IMongoDatabase database)
        {
            var collection = database.GetCollection<EventInstance>("events");
            var indexDefinitionCreatedDateTime = Builders<EventInstance>.IndexKeys.Ascending(x => x.CreatedDateTime);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<EventInstance>(indexDefinitionCreatedDateTime));
        }

        private async Task InitialiseConsoleSettings(IMongoDatabase database)
        {
            //var collection = database.GetCollection<ConsoleEventSettings>("console_event_settings");
            //var indexDefinition = Builders<ConsoleEventSettings>.IndexKeys.Ascending(x => x.Name);
            //await collection.Indexes.CreateOneAsync(new CreateIndexModel<EventHandlerRule>(indexDefinition));
        }

        public async Task LoadData(int group)
        {
            switch(group)
            {
                case 1:            
                    await LoadData1();
                    break;
            }
        }
        
        public async Task DeleteAllData()
        {
            await _eventClientService.DeleteAllAsync();
            await _eventHandlerService.DeleteAllAsync();
            await _eventService.DeleteAllAsync();
            await _eventTypeService.DeleteAllAsync();            

            await _consoleSettingsService.DeleteAllAsync();
            await _csvSettingsService.DeleteAllAsync();
            await _emailSettingsService.DeleteAllAsync();
            await _httpSettingsService.DeleteAllAsync();
            await _processSettingsService.DeleteAllAsync();
            await _smsSettingsService.DeleteAllAsync();
            await _sqlSettingsService.DeleteAllAsync();
            await _teamsSettingsService.DeleteAllAsync();
        }

        /// <summary>
        /// Loads data for group 1
        /// </summary>
        /// <returns></returns>
        private async Task LoadData1()
        {
            await DeleteAllData();

            // Base data
            await _eventClientService.ImportAsync(new EventClientSeed1());
            await _eventHandlerService.ImportAsync(new EventHandlerSeed1());
            await _eventTypeService.ImportAsync(new EventTypeSeed1());

            // Event settings
            await _consoleSettingsService.ImportAsync(new ConsoleEventSettingsSeed1());
            await _csvSettingsService.ImportAsync(new CSVEventSettingsSeed1());
            await _emailSettingsService.ImportAsync(new EmailEventSettingsSeed1());
            await _httpSettingsService.ImportAsync(new HTTPEventSettingsSeed1());
            await _processSettingsService.ImportAsync(new ProcessEventSettingsSeed1());
            await _smsSettingsService.ImportAsync(new SMSEventSettingsSeed1());
            await _sqlSettingsService.ImportAsync(new SQLEventSettingsSeed1());
            await _teamsSettingsService.ImportAsync(new TeamsEventSettingsSeed1());

            // Event handler rules. Needs to be done at the end because it depends on event settings etc
            await _eventHandlerRuleService.ImportAsync(new EventHandlerRuleSeed1(_consoleSettingsService, _csvSettingsService,
                           _emailSettingsService, _eventHandlerService, _eventTypeService,
                           _httpSettingsService, _processSettingsService, _signalRSettingsService,
                           _smsSettingsService, _sqlSettingsService, _teamsSettingsService));
        }
    }
}
