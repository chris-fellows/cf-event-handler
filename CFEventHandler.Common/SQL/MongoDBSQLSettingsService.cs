using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using CFEventHandler.SMS;
using MongoDB.Driver;

namespace CFEventHandler.SQL
{
    public class MongoDBSQLSettingsService : MongoDBBaseService<SQLEventSettings>, ISQLSettingsService
    {
        public MongoDBSQLSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "sql_event_settings")
        {

        }

        public Task<SQLEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<SQLEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
