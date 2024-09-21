using CFEventHandler.Interfaces;
using CFEventHandler.Process;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.SignalR
{
    public class MongoDBSignalRSettingsService : MongoDBBaseService<SignalREventSettings>, ISignalRSettingsService
    {
        public MongoDBSignalRSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "signalr_event_settings")
        {

        }

        public Task<SignalREventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<SignalREventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
