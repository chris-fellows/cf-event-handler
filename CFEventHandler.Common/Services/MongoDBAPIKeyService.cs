using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.Common.Services
{
    public class MongoDBAPIKeyService : MongoDBBaseService<APIKeyInstance>, IAPIKeyService
    {        
        public MongoDBAPIKeyService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "api_keys")
        {
            
        }
       
        public Task<APIKeyInstance?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<APIKeyInstance?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }       

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
