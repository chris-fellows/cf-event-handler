using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using CFEventHandler.SQL;
using MongoDB.Driver;

namespace CFEventHandler.Teams
{
    public class MongoDBTeamsSettingsService : MongoDBBaseService<TeamsEventSettings>, ITeamsSettingsService
    {
        public MongoDBTeamsSettingsService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "teams_event_settings")
        {

        }

        public Task<TeamsEventSettings?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<TeamsEventSettings?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
