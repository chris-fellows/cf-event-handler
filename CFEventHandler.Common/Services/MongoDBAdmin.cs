using CFEventHandler.Common.Interfaces;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandlerObject = CFEventHandler.Models.EventHandler;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBAdmin : IDatabaseAdmin
    {
        private readonly IDatabaseConfig _databaseConfig;

        public MongoDBAdmin(IDatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
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
    }
}
