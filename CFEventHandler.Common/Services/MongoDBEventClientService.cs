using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBEventClientService : IEventClientService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<EventClient> _eventClients;

        public MongoDBEventClientService(ITenantDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventClients = database.GetCollection<EventClient>("event_clients");
        }

        public async Task ImportAsync(IEntityList<EventClient> eventClientList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventClients.InsertManyAsync(eventClientList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<EventClient> eventClientList)
        {
            eventClientList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<EventClient> GetAll()
        {
            return _eventClients.Find(x => true).ToEnumerable();
        }

        public Task<EventClient?> GetByIdAsync(string id)
        {
            return _eventClients.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventClient?> GetByNameAsync(string name)
        {
            return _eventClients.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task<EventClient> AddAsync(EventClient eventType)
        {
            _eventClients.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }

        public async Task DeleteAllAsync()
        {
            await _eventClients.DeleteManyAsync(Builders<EventClient>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventClients.DeleteOneAsync(id);
        }
    }
}
