using CFEventHandler.Email;
using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.HTTP
{
    public class MongoDBHTTPSettingsService : MongoDBBaseService<HTTPEventSettings>, IHTTPSettingsService
    {
        public MongoDBHTTPSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "http_event_settings")
        {

        }

        public Task<HTTPEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<HTTPEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
