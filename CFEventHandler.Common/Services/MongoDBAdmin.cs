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
using CFEventHandler.Common.Seed;
using Microsoft.Extensions.DependencyInjection;
using CFUtilities.Encryption;
using System.Text;

namespace CFEventHandler.Services
{
    public class MongoDBAdmin : IDatabaseAdmin
    {
        private readonly IAPIKeyService _apiKeyService;
        private readonly IDatabaseConfig _databaseConfig;       
        private readonly ITenantService _tenantService;
        
        private readonly IServiceProvider _serviceProvider;

        private class MyTenantDatabaseConfig : ITenantDatabaseConfig
        {
            public string TenantId { get; set; } = String.Empty;

            private string _connectionString = String.Empty;

            // TODO: Clean this up
            public string ConnectionString
            {
                set { _connectionString = value; }
                get
                {
                    const string Key = "k8Bv29sHy0aYvAq56a";    // TODO: Move elsewhere (Environment variable etc)
                    const string EncryptedPrefix = "\tEncrypted\t";
                    return Encoding.UTF8.GetString(TripleDESEncryption.DecryptByteArray(System.Convert.FromBase64String(_connectionString.Substring(EncryptedPrefix.Length)), Key));
                }
            }

            public string DatabaseName { get; set; } = String.Empty;
        }

        public MongoDBAdmin(IAPIKeyService apiKeyService,
                        IDatabaseConfig databaseConfig,                       
                        IServiceProvider serviceProvider,                        
                        ITenantService tenantService)
        {
            _apiKeyService = apiKeyService;
            _databaseConfig = databaseConfig;         
            _serviceProvider = serviceProvider;            
            _tenantService = tenantService;            
        }        

        public async Task InitialiseAsync()
        {            
            // Configure main DB
            var client = new MongoClient(_databaseConfig.ConnectionString);
            var database = client.GetDatabase(_databaseConfig.DatabaseName);

            await InitialiseTenants(database);
            await InitialiseAPIKeys(database);

            // Configure tenant datanases
            var tenants = _tenantService.GetAll();
            foreach (var tenant in tenants)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    ITenantDatabaseConfig tenantDatabaseConfig = new MyTenantDatabaseConfig()
                    {
                        ConnectionString = tenant.ConnectionString,
                        DatabaseName = tenant.DatabaseName,
                    };

                    var tenantClient = new MongoClient(tenantDatabaseConfig.ConnectionString);
                    var tenantDatabase = tenantClient.GetDatabase(tenantDatabaseConfig.DatabaseName);

                    // Initialise collections            
                    await InitialiseEventClients(tenantDatabase);
                    await InitialiseEventTypes(tenantDatabase);
                    await InitialiseEventHandlers(tenantDatabase);
                    await InitialiseEventHandlerRules(tenantDatabase);
                    await InitialiseEvents(tenantDatabase);
                }
            }
        }

        private async Task InitialiseAPIKeys(IMongoDatabase database)
        {
            var collection = database.GetCollection<APIKeyInstance>("api_keys");
            var indexDefinitionName = Builders<APIKeyInstance>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<APIKeyInstance>(indexDefinitionName));
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

        private async Task InitialiseTenants(IMongoDatabase database)
        {
            var collection = database.GetCollection<Tenant>("tenants");
            var indexDefinitionName = Builders<Tenant>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<Tenant>(indexDefinitionName));
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
            // Configure main DB
            var client = new MongoClient(_databaseConfig.ConnectionString);
            var database = client.GetDatabase(_databaseConfig.DatabaseName);

            // Load to main DB
            using (var scope = _serviceProvider.CreateScope())
            {
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var apiKeyService = scope.ServiceProvider.GetRequiredService<IAPIKeyService>();
                var testCurrentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();

                await apiKeyService.DeleteAllAsync();
                await tenantService.DeleteAllAsync();
            }

            // Load data in to tenant DBs
            var tenants = _tenantService.GetAll();
            foreach (var tenant in tenants)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Set correct tenant for DI
                    ITenantDatabaseConfig tenantDatabaseConfig = new MyTenantDatabaseConfig()
                    {
                        TenantId = tenant.Id,
                        ConnectionString = tenant.ConnectionString,
                        DatabaseName = tenant.DatabaseName
                    };
                    var currentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                    currentTenantContext.TenantDatabaseConfig = tenantDatabaseConfig;

                    var documentTemplateService = scope.ServiceProvider.GetRequiredService<IDocumentTemplateService>();
                    var eventClientService = scope.ServiceProvider.GetRequiredService<IEventClientService>();
                    var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();
                    var eventHandlerRuleService = scope.ServiceProvider.GetRequiredService<IEventHandlerRuleService>();
                    var eventTypeService = scope.ServiceProvider.GetRequiredService<IEventTypeService>();

                    var consoleSettingsService = scope.ServiceProvider.GetRequiredService<IConsoleSettingsService>();
                    var csvSettingsService = scope.ServiceProvider.GetRequiredService<ICSVSettingsService>();
                    var emailSettingsService = scope.ServiceProvider.GetRequiredService<IEmailSettingsService>();
                    var httpSettingsService = scope.ServiceProvider.GetRequiredService<IHTTPSettingsService>();
                    var processSettingsService = scope.ServiceProvider.GetRequiredService<IProcessSettingsService>();
                    var signalRSettingsService = scope.ServiceProvider.GetRequiredService<ISignalRSettingsService>();
                    var smsSettingsService = scope.ServiceProvider.GetRequiredService<ISMSSettingsService>();
                    var sqlSettingsService = scope.ServiceProvider.GetRequiredService<ISQLSettingsService>();
                    var teamsSettingsService = scope.ServiceProvider.GetRequiredService<ITeamsSettingsService>();

                    // Base data                    
                    await documentTemplateService.DeleteAllAsync();
                    await eventClientService.DeleteAllAsync();
                    await eventHandlerService.DeleteAllAsync();
                    await eventTypeService.DeleteAllAsync();

                    // Event settings
                    await consoleSettingsService.DeleteAllAsync();
                    await csvSettingsService.DeleteAllAsync();
                    await emailSettingsService.DeleteAllAsync();
                    await httpSettingsService.DeleteAllAsync();
                    await processSettingsService.DeleteAllAsync();
                    await smsSettingsService.DeleteAllAsync();
                    await sqlSettingsService.DeleteAllAsync();
                    await teamsSettingsService.DeleteAllAsync();
                    
                    await eventHandlerRuleService.DeleteAllAsync();
                }
            }

            int xxx = 1000;
        }

        /// <summary>
        /// Loads data for group 1
        /// </summary>
        /// <returns></returns>
        private async Task LoadData1()
        {
            // Configure main DB
            //var client = new MongoClient(_databaseConfig.ConnectionString);
            //var database = client.GetDatabase(_databaseConfig.DatabaseName);

            // Load to main DB
            using (var scope = _serviceProvider.CreateScope())            
            {
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var apiKeyService = scope.ServiceProvider.GetRequiredService<IAPIKeyService>();
                var testCurrentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();

                apiKeyService.ImportAsync(new APIKeySeed1());
                tenantService.ImportAsync(new TenantSeed1());
            }

            // Load data in to tenant DBs
            var tenants = _tenantService.GetAll();
            
            foreach (var tenant in tenants)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Set correct tenant for DI
                    ITenantDatabaseConfig tenantDatabaseConfig = new MyTenantDatabaseConfig()
                    {
                        TenantId = tenant.Id,
                        ConnectionString = tenant.ConnectionString,
                        DatabaseName = tenant.DatabaseName
                    };
                    var currentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                    currentTenantContext.TenantDatabaseConfig = tenantDatabaseConfig;

                    var documentTemplateService = scope.ServiceProvider.GetRequiredService<IDocumentTemplateService>();
                    var eventClientService = scope.ServiceProvider.GetRequiredService<IEventClientService>();
                    var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();
                    var eventHandlerRuleService =scope.ServiceProvider.GetRequiredService<IEventHandlerRuleService>();
                    var eventTypeService = scope.ServiceProvider.GetRequiredService<IEventTypeService>();

                    var consoleSettingsService = scope.ServiceProvider.GetRequiredService<IConsoleSettingsService>();
                    var csvSettingsService = scope.ServiceProvider.GetRequiredService<ICSVSettingsService>();
                    var emailSettingsService = scope.ServiceProvider.GetRequiredService<IEmailSettingsService>();
                    var httpSettingsService = scope.ServiceProvider.GetRequiredService<IHTTPSettingsService>();
                    var processSettingsService = scope.ServiceProvider.GetRequiredService<IProcessSettingsService>();
                    var signalRSettingsService = scope.ServiceProvider.GetRequiredService<ISignalRSettingsService>();
                    var smsSettingsService = scope.ServiceProvider.GetRequiredService<ISMSSettingsService>();
                    var sqlSettingsService = scope.ServiceProvider.GetRequiredService<ISQLSettingsService>();
                    var teamsSettingsService = scope.ServiceProvider.GetRequiredService<ITeamsSettingsService>();

                    // Base data                    
                    await documentTemplateService.ImportAsync(new DocumentTemplateSeed1());
                    await eventClientService.ImportAsync(new EventClientSeed1());
                    await eventHandlerService.ImportAsync(new EventHandlerSeed1());
                    await eventTypeService.ImportAsync(new EventTypeSeed1());
                    
                    // Event settings
                    await consoleSettingsService.ImportAsync(new ConsoleEventSettingsSeed1());
                    await csvSettingsService.ImportAsync(new CSVEventSettingsSeed1());
                    await emailSettingsService.ImportAsync(new EmailEventSettingsSeed1(documentTemplateService));
                    await httpSettingsService.ImportAsync(new HTTPEventSettingsSeed1());
                    await processSettingsService.ImportAsync(new ProcessEventSettingsSeed1());
                    await smsSettingsService.ImportAsync(new SMSEventSettingsSeed1());
                    await sqlSettingsService.ImportAsync(new SQLEventSettingsSeed1());
                    await teamsSettingsService.ImportAsync(new TeamsEventSettingsSeed1());

                    // Event handler rules. Needs to be done at the end because it depends on event settings etc
                    await eventHandlerRuleService.ImportAsync(new EventHandlerRuleSeed1(consoleSettingsService, csvSettingsService,
                                   emailSettingsService, eventHandlerService, eventTypeService,
                                   httpSettingsService, processSettingsService, signalRSettingsService,
                                   smsSettingsService, sqlSettingsService, teamsSettingsService));
                }
            }

            int xxx = 1000;
            //await DeleteAllData();            
        }
    }
}
