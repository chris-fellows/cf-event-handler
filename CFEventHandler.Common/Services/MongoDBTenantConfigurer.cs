using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using CFEventHandlerObject = CFEventHandler.Models.EventHandler;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBTenantConfigurer : ITenantDatabaseConfigurer
    {  
        private readonly IDatabaseConfig _databaseConfig;
        private readonly ITenantService _tenantService;

        private readonly IServiceProvider _serviceProvider;

        public MongoDBTenantConfigurer(IDatabaseConfig databaseConfig,
                        IServiceProvider serviceProvider,
                        ITenantService tenantService)
        {
            //_apiKeyService = apiKeyService;
            _databaseConfig = databaseConfig;
            _serviceProvider = serviceProvider;
            _tenantService = tenantService;
        }       

        public async Task InitialiseAsync(string tenantId)
        {
            var tenant = await _tenantService.GetByIdAsync(tenantId);

            using (var tenantScope = _serviceProvider.CreateScope())
            {
                ITenantDatabaseConfig tenantDatabaseConfig = new TenantDatabaseConfig()
                {
                    ConnectionString = tenant.ConnectionString,
                    DatabaseName = tenant.DatabaseName,
                    TenantId = tenant.Id
                };

                var tenantClient = new MongoClient(tenantDatabaseConfig.ConnectionString);
                var tenantDatabase = tenantClient.GetDatabase(tenantDatabaseConfig.DatabaseName);

                // Initialise collections
                await InitialiseAPIKeys(tenantDatabase);
                await InitialiseEventClients(tenantDatabase);
                await InitialiseEventTypes(tenantDatabase);
                await InitialiseEventHandlers(tenantDatabase);
                await InitialiseEventHandlerRules(tenantDatabase);
                await InitialiseEvents(tenantDatabase);

                // Initialise event settings collections
                await InitialiseConsoleEventSettings(tenantDatabase);
                await InitialiseCSVEventSettings(tenantDatabase);
                await InitialiseEmailEventSettings(tenantDatabase);
                await InitialiseHTTPEventSettings(tenantDatabase);
                await InitialiseProcessEventSettings(tenantDatabase);
                await InitialiseSignalREventSettings(tenantDatabase);
                await InitialiseSMSEventSettings(tenantDatabase);
                await InitialiseSQLEventSettings(tenantDatabase);
                await InitialiseTeamsEventSettings(tenantDatabase);
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

        private async Task InitialiseConsoleEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<ConsoleEventSettings>("console_event_settings");
            var indexDefinition = Builders<ConsoleEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<ConsoleEventSettings>(indexDefinition));
        }

        private async Task InitialiseCSVEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<CSVEventSettings>("csv_event_settings");
            var indexDefinition = Builders<CSVEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<CSVEventSettings>(indexDefinition));
        }

        private async Task InitialiseEmailEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<EmailEventSettings>("email_event_settings");
            var indexDefinition = Builders<EmailEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<EmailEventSettings>(indexDefinition));
        }

        private async Task InitialiseHTTPEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<HTTPEventSettings>("email_event_settings");
            var indexDefinition = Builders<HTTPEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<HTTPEventSettings>(indexDefinition));
        }

        private async Task InitialiseProcessEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<ProcessEventSettings>("process_event_settings");
            var indexDefinition = Builders<ProcessEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<ProcessEventSettings>(indexDefinition));
        }

        private async Task InitialiseSignalREventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<SignalREventSettings>("signalr_event_settings");
            var indexDefinition = Builders<SignalREventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<SignalREventSettings>(indexDefinition));
        }

        private async Task InitialiseSMSEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<SMSEventSettings>("sms_event_settings");
            var indexDefinition = Builders<SMSEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<SMSEventSettings>(indexDefinition));
        }

        private async Task InitialiseSQLEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<SQLEventSettings>("sql_event_settings");
            var indexDefinition = Builders<SQLEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<SQLEventSettings>(indexDefinition));
        }

        private async Task InitialiseTeamsEventSettings(IMongoDatabase database)
        {
            var collection = database.GetCollection<TeamsEventSettings>("teams_event_settings");
            var indexDefinition = Builders<TeamsEventSettings>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<TeamsEventSettings>(indexDefinition));
        }

        private async Task InitialiseTenants(IMongoDatabase database)
        {
            var collection = database.GetCollection<Tenant>("tenants");
            var indexDefinitionName = Builders<Tenant>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<Tenant>(indexDefinitionName));
        }
    }
}
