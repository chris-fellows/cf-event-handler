using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBEventTypeService : MongoDBBaseService<EventType>, IEventTypeService
    {                
        public MongoDBEventTypeService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "event_types")
        {            
            
        }      

        public Task<EventType?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventType?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
