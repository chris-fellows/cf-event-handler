using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Services;
using MongoDB.Driver;

namespace CFEventHandler.Console
{
    public class MongoDBConsoleSettingsService : MongoDBBaseService<ConsoleEventSettings>, IConsoleSettingsService
    {        
        public MongoDBConsoleSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "console_event_settings")
        {
         
        }
   
        public Task<ConsoleEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<ConsoleEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
