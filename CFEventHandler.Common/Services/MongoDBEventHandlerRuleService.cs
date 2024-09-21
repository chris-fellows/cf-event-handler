using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using System;
using System.ComponentModel;

namespace CFEventHandler.Services
{
    public class MongoDBEventHandlerRuleService : MongoDBBaseService<EventHandlerRule>, IEventHandlerRuleService
    {        
        public MongoDBEventHandlerRuleService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "event_handler_rules")
        {
            
        }

        public Task<EventHandlerRule?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventHandlerRule?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
