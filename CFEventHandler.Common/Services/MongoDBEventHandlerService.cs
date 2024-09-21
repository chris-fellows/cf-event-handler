using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using CFEventHandlerObject = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Services
{
    public class MongoDBEventHandlerService : MongoDBBaseService<CFEventHandlerObject>, IEventHandlerService
    {        
        public MongoDBEventHandlerService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "event_handlers")
        {
          
        }

        public Task<CFEventHandlerObject?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<CFEventHandlerObject?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }    

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
