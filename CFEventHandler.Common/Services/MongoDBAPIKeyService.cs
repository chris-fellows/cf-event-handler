using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Common.Services
{
    public class MongoDBAPIKeyService : IAPIKeyService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<APIKeyInstance> _apiKeys;

        public MongoDBAPIKeyService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _apiKeys = database.GetCollection<APIKeyInstance>("api_kets");
        }

        public async Task ImportAsync(IEntityList<APIKeyInstance> apiKeys)
        {            
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _apiKeys.InsertManyAsync(apiKeys.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<APIKeyInstance> apiKeys)
        {
            apiKeys.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<APIKeyInstance> GetAll()
        {
            return _apiKeys.Find(x => true).ToEnumerable();
        }

        public Task<APIKeyInstance?> GetByIdAsync(string id)
        {
            return _apiKeys.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<APIKeyInstance?> GetByNameAsync(string name)
        {
            return _apiKeys.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task<APIKeyInstance> AddAsync(APIKeyInstance eventType)
        {
            _apiKeys.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }

        public async Task DeleteAllAsync()
        {
            await _apiKeys.DeleteManyAsync(Builders<APIKeyInstance>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _apiKeys.DeleteOneAsync(id);
        }
    }
}
