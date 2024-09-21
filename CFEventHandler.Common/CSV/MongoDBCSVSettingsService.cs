using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.CSV
{
    public class MongoDBCSVSettingsService : MongoDBBaseService<CSVEventSettings>, ICSVSettingsService
    {    
        public MongoDBCSVSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "csv_event_settings")
        {
           
        }

        public Task<CSVEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<CSVEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
