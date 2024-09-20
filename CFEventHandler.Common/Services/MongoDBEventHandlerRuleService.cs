using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using System;
using System.ComponentModel;

namespace CFEventHandler.Services
{
    public class MongoDBEventHandlerRuleService : IEventHandlerRuleService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<EventHandlerRule> _eventHandlerRules;

        public MongoDBEventHandlerRuleService(ITenantDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventHandlerRules = database.GetCollection<EventHandlerRule>("event_handler_rules");
        }

        public async Task ImportAsync(IEntityList<EventHandlerRule> eventHandlerRuleList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventHandlerRules.InsertManyAsync(eventHandlerRuleList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<EventHandlerRule> eventHandlerRuleList)
        {
            eventHandlerRuleList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<EventHandlerRule> GetAll()
        {
            return _eventHandlerRules.Find(x => true).ToEnumerable();
        }

        public Task<EventHandlerRule?> GetByIdAsync(string id)
        {
            return _eventHandlerRules.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventHandlerRule> AddAsync(EventHandlerRule eventHandlerRule)
        {
            _eventHandlerRules.InsertOneAsync(eventHandlerRule);
            return Task.FromResult(eventHandlerRule);
        }

        public async Task DeleteAllAsync()
        {
            await _eventHandlerRules.DeleteManyAsync(Builders<EventHandlerRule>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventHandlerRules.DeleteOneAsync(id);
        }
    }
}
