using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using CFEventHandler.SignalR;
using MongoDB.Driver;

namespace CFEventHandler.SMS
{
    public class MongoDBSMSSettingsService : MongoDBBaseService<SMSEventSettings>, ISMSSettingsService
    {
        public MongoDBSMSSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "sms_event_settings")
        {

        }

        public Task<SMSEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<SMSEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
