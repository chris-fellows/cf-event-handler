
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFUtilities.Utilities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace CFEventHandler.Services
{
    public class MongoDBEventService : MongoDBBaseService<EventInstance>, IEventService
    {        
        public MongoDBEventService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "events")
        {
      
        }
      
        public Task<EventInstance?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventInstance?> GetByNameAsync(string name)
        {
            return null;    // Event does not have Name property
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
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
            var events = await _entities.Find(filterDefinition)
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
            await _entities.DeleteManyAsync(filterDefinition);            
        }

        public async Task<List<EventSummary>> GetEventSummary(EventFilter eventFilter)
        {
            var filterDefinition = GetFilterDefinition(eventFilter);

            //// Defines the aggregation pipeline with the $match and $group aggregation stages
            //var pipeline = new EmptyPipelineDefinition<EventInstance>()
            //    .Match(filterDefinition)
            //    .Group(r => new { Date = r.CreatedDateTime.Date, EventClientId = r.EventClientId, EventTypeId = r.EventTypeId },
            //        g => new
            //        {
            //            Date = g.Key.Date,
            //            EventClientId = g.Key.EventClientId,
            //            EventTypeId = g.Key.EventTypeId,
            //            Count = g.Count()
            //        }
            //    );

            // Defines the aggregation pipeline with the $match and $group aggregation stages
            var pipeline = new EmptyPipelineDefinition<EventInstance>()
                .Match(filterDefinition)
                .Group(r => r.CreatedDateTime,
                    g => new
                    {
                        Date = g.Key.Date,
                        EventClientId = "X",
                        EventTypeId = "Y",
                        Count = g.Count()
                    }
                );

            // Executes the aggregation pipeline
            var results = (await _entities.AggregateAsync(pipeline)).ToListAsync().Result;

            var eventSummaries = results.Select(r => new EventSummary() { Date = r.Date, EventClientId = r.EventClientId, EventTypeId  = r.EventTypeId, Count = r.Count }).ToList();
            
            return eventSummaries;
        }
    }
}
