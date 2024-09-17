using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBEventTypeService : IEventTypeService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<EventType> _eventTypes;
        
        public MongoDBEventTypeService(IDatabaseConfig databaseConfig)
        {            
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventTypes = database.GetCollection<EventType>("event_types");
        }

        public async Task ImportAsync(IEntityList<EventType> eventTypeList)
        {
            //    return _eventTypes.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventTypes.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<EventType> eventTypeList)
        {
            eventTypeList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }
        
        public IEnumerable<EventType> GetAll()
        {
            return _eventTypes.Find(x => true).ToEnumerable();
        }

        public Task<EventType?> GetByIdAsync(string id)
        {
            return _eventTypes.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventType?> GetByNameAsync(string name)
        {
            return _eventTypes.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task<EventType> AddAsync(EventType eventType)
        {
            _eventTypes.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }
        
        public async Task DeleteAllAsync()
        {            
            await _eventTypes.DeleteManyAsync(Builders<EventType>.Filter.Empty);            
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventTypes.DeleteOneAsync(id);
        }
    }
}
