using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Services
{
    public class MongoDBTenantService : ITenantService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<Tenant> _tenants;

        public MongoDBTenantService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _tenants = database.GetCollection<Tenant>("tenants");
        }

        public async Task ImportAsync(IEntityList<Tenant> eventTypeList)
        {  
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _tenants.InsertManyAsync(eventTypeList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<Tenant> eventTypeList)
        {
            eventTypeList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _tenants.Find(x => true).ToEnumerable();
        }

        public Task<Tenant?> GetByIdAsync(string id)
        {
            return _tenants.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<Tenant?> GetByNameAsync(string name)
        {
            return _tenants.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task<Tenant> AddAsync(Tenant eventType)
        {
            _tenants.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }

        public async Task DeleteAllAsync()
        {
            await _tenants.DeleteManyAsync(Builders<Tenant>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _tenants.DeleteOneAsync(id);
        }
    }
}
