using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using System;

namespace CFEventHandler.Services
{
    public class MongoDBEventService : IEventService
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<EventInstance> _eventInstances;

        public MongoDBEventService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventInstances = database.GetCollection<EventInstance>("events");
        }

        public async Task ImportAsync(IEntityList<EventInstance> eventTypeList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventInstances.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
            //return _eventInstances.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
        }

        public Task ExportAsync(IEntityList<EventInstance> eventTypeList)
        {
            eventTypeList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<EventInstance> GetAll()
        {
            return _eventInstances.Find(x => true).ToEnumerable();
        }

        public Task<EventInstance?> GetByIdAsync(string id)
        {
            return _eventInstances.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventInstance> AddAsync(EventInstance eventInstance)
        {
            _eventInstances.InsertOneAsync(eventInstance);
            return Task.FromResult(eventInstance);
        }

        public async Task DeleteAllAsync()
        {
            await _eventInstances.DeleteManyAsync(Builders<EventInstance>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventInstances.DeleteOneAsync(id);
        }

        public async Task<List<EventInstance>> GetByFilter(EventFilter eventFilter)
        {
            return new List<EventInstance>();           
        }
    }
}
