using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using CFEventHandlerObject = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Services
{
    public class MongoDBEventHandlerService : IEventHandlerService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<CFEventHandlerObject> _eventHandlers;

        public MongoDBEventHandlerService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventHandlers = database.GetCollection<CFEventHandlerObject>("event_handlers");
        }

        public async Task ImportAsync(IEntityList<CFEventHandlerObject> eventHandlerList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventHandlers.InsertManyAsync(eventHandlerList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
            //return _eventHandlers.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
        }

        public Task ExportAsync(IEntityList<CFEventHandlerObject> eventHandlerList)
        {
            eventHandlerList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<CFEventHandlerObject> GetAll()
        {
            return _eventHandlers.Find(x => true).ToEnumerable();
        }

        public Task<CFEventHandlerObject?> GetByIdAsync(string id)
        {
            return _eventHandlers.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<CFEventHandlerObject?> GetByNameAsync(string name)
        {
            return _eventHandlers.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task<CFEventHandlerObject> AddAsync(CFEventHandlerObject eventType)
        {
            _eventHandlers.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }

        public async Task DeleteAllAsync()
        {
            await _eventHandlers.DeleteManyAsync(Builders<CFEventHandlerObject>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventHandlers.DeleteOneAsync(id);
        }
    }
}
