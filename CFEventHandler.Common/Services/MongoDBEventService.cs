
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFUtilities.Utilities;
using MongoDB.Driver;
using System;

namespace CFEventHandler.Services
{
    public class MongoDBEventService : IEventService
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<EventInstance> _eventInstances;

        public MongoDBEventService(ITenantDatabaseConfig databaseConfig)
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
            if (eventFilter.PageItems < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(eventFilter.PageItems));
            }
            if (eventFilter.PageNo < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(eventFilter.PageNo));
            }

            // Get filter definition
            var filterDefinition = GetFilterDefinition(eventFilter);            

            // Get filtered events page
            var events = await _eventInstances.Find(filterDefinition)
                            .SortBy(x => x.CreatedDateTime)                            
                            .Skip(NumericUtilities.GetPageSkip(eventFilter.PageItems, eventFilter.PageNo))                           
                            .Limit(eventFilter.PageItems)
                            .ToListAsync();

            //var events = await _eventInstances.FindAsync(filter);

            return events;
        }

        /// <summary>
        /// Returns MongoDB filter definition for EventFilter       
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        private static FilterDefinition<EventInstance> GetFilterDefinition(EventFilter eventFilter)
        {
            // Set date range filter
            var filterDefinition = Builders<EventInstance>.Filter.Gte(x => x.CreatedDateTime, eventFilter.FromCreatedDateTime.UtcDateTime);
            filterDefinition = filterDefinition & Builders<EventInstance>.Filter.Lte(x => x.CreatedDateTime, eventFilter.ToCreatedDateTime.UtcDateTime);

            // Filter event types
            if (eventFilter.EventTypeIds != null && eventFilter.EventTypeIds.Any())
            {
                filterDefinition = filterDefinition & Builders<EventInstance>.Filter.In(x => x.EventTypeId, eventFilter.EventTypeIds.ToArray());
            }

            // Filter event clients
            if (eventFilter.EventClientIds != null && eventFilter.EventClientIds.Any())
            {
                filterDefinition = filterDefinition & Builders<EventInstance>.Filter.In(x => x.EventClientId, eventFilter.EventClientIds.ToArray());
            }

            //var sort = Builders<EventInstance>.Sort.Ascending(x => x.CreatedDateTime);

            return filterDefinition;
        }

        public async Task DeleteByFilter(EventFilter eventFilter)
        {            
            // Get filter definition
            var filterDefinition = GetFilterDefinition(eventFilter);

            // Delete
            await _eventInstances.DeleteManyAsync(filterDefinition);            
        }
    }
}
