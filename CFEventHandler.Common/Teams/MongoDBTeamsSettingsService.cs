using CFEventHandler.Interfaces;
using MongoDB.Driver;

namespace CFEventHandler.Teams
{
    public class MongoDBTeamsSettingsService : ITeamsSettingsService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<TeamsEventSettings> _eventSettings;

        public MongoDBTeamsSettingsService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventSettings = database.GetCollection<TeamsEventSettings>("teams_event_settings");
        }

        public async Task ImportAsync(IEntityList<TeamsEventSettings> eventSettingsList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventSettings.InsertManyAsync(eventSettingsList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<TeamsEventSettings> eventSettingsList)
        {
            eventSettingsList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<TeamsEventSettings> GetAll()
        {
            return _eventSettings.Find(x => true).ToEnumerable();
        }

        public Task<TeamsEventSettings?> GetByIdAsync(string id)
        {
            return _eventSettings.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<TeamsEventSettings> AddAsync(TeamsEventSettings eventSettings)
        {
            _eventSettings.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _eventSettings.DeleteManyAsync(Builders<TeamsEventSettings>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventSettings.DeleteOneAsync(id);
        }
    }
}
