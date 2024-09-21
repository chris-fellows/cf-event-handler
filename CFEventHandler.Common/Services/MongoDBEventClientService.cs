using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBEventClientService : MongoDBBaseService<EventClient>, IEventClientService
    {
        public MongoDBEventClientService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "event_clients")
        {
         
        }    

        public Task<EventClient?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EventClient?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
