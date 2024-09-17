using CFEventHandler.Interfaces;
using MongoDB.Driver;

namespace CFEventHandler.SignalR
{
    public class MongoDBSignalRSettingsService : ISignalRSettingsService
    {
        private readonly MongoClient? _client;           
        private readonly IMongoCollection<SignalREventSettings> _eventSettings;

        public MongoDBSignalRSettingsService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventSettings = database.GetCollection<SignalREventSettings>("signalr_event_settings");
        }

        public async Task ImportAsync(IEntityList<SignalREventSettings> eventSettingsList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventSettings.InsertManyAsync(eventSettingsList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<SignalREventSettings> eventSettingsList)
        {
            eventSettingsList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<SignalREventSettings> GetAll()
        {
            return _eventSettings.Find(x => true).ToEnumerable();
        }

        public Task<SignalREventSettings?> GetByIdAsync(string id)
        {
            return _eventSettings.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<SignalREventSettings> AddAsync(SignalREventSettings eventSettings)
        {
            _eventSettings.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _eventSettings.DeleteManyAsync(Builders<SignalREventSettings>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventSettings.DeleteOneAsync(id);
        }
    }
}
