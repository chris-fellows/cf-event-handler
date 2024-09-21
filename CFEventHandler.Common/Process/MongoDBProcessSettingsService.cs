using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.Process
{
    public class MongoDBProcessSettingsService : MongoDBBaseService<ProcessEventSettings>, IProcessSettingsService
    {
        public MongoDBProcessSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "process_event_settings")
        {

        }

        public Task<ProcessEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<ProcessEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
