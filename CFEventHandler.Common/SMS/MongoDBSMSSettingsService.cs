using CFEventHandler.Interfaces;
using MongoDB.Driver;

namespace CFEventHandler.SMS
{
    public class MongoDBSMSSettingsService : ISMSSettingsService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<SMSEventSettings> _eventSettings;

        public MongoDBSMSSettingsService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventSettings = database.GetCollection<SMSEventSettings>("sms_event_settings");
        }

        public async Task ImportAsync(IEntityList<SMSEventSettings> eventSettingsList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventSettings.InsertManyAsync(eventSettingsList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<SMSEventSettings> eventSettingsList)
        {
            eventSettingsList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<SMSEventSettings> GetAll()
        {
            return _eventSettings.Find(x => true).ToEnumerable();
        }

        public Task<SMSEventSettings?> GetByIdAsync(string id)
        {
            return _eventSettings.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<SMSEventSettings> AddAsync(SMSEventSettings eventSettings)
        {
            _eventSettings.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _eventSettings.DeleteManyAsync(Builders<SMSEventSettings>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventSettings.DeleteOneAsync(id);
        }
    }
}
