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
            // Set date range filter
            var filter = Builders<EventInstance>.Filter.Gte(x => x.CreatedDateTime, eventFilter.FromCreatedDateTime.UtcDateTime);
            filter = filter & Builders<EventInstance>.Filter.Lte(x => x.CreatedDateTime, eventFilter.ToCreatedDateTime.UtcDateTime);

            /*
            // Filter event types
            if (eventFilter.EventTypeIds != null && eventFilter.EventTypeIds.Any())
            {
                filter = filter & Builders<EventInstance>.Filter.StringIn(x => x.EventTypeId, eventFilter.EventTypeIds.ToArray());
            }
            */

            /*
            // Filter event clients
            if (eventFilter.EventClientIds != null && eventFilter.EventClientIds.Any())
            {
                filter = filter & Builders<EventInstance>.Filter.StringIn(x => x.EventClientId, eventFilter.EventClientIds.ToArray());
            }
            */

            var events = await _eventInstances.FindAsync(filter);

            return await events.ToListAsync();
        }

        public async Task DeleteByFilter(EventFilter eventFilter)
        {
            // Set date range filter
            var filter = Builders<EventInstance>.Filter.Gte(x => x.CreatedDateTime, eventFilter.FromCreatedDateTime.UtcDateTime);
            filter = filter & Builders<EventInstance>.Filter.Lte(x => x.CreatedDateTime, eventFilter.ToCreatedDateTime.UtcDateTime);

            /*
            // Filter event types
            if (eventFilter.EventTypeIds != null && eventFilter.EventTypeIds.Any())
            {
                filter = filter & Builders<EventInstance>.Filter.StringIn(x => x.EventTypeId, eventFilter.EventTypeIds.ToArray());
            }
            */

            /*
            // Filter event clients
            if (eventFilter.EventClientIds != null && eventFilter.EventClientIds.Any())
            {
                filter = filter & Builders<EventInstance>.Filter.StringIn(x => x.EventClientId, eventFilter.EventClientIds.ToArray());
            }
            */

            await _eventInstances.DeleteManyAsync(filter);            
        }
    }
}
