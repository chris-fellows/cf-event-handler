using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.Email
{
    public class MongoDBEmailSettingsService : MongoDBBaseService<EmailEventSettings>, IEmailSettingsService
    {
        public MongoDBEmailSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "email_event_settings")
        {

        }

        public Task<EmailEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EmailEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
