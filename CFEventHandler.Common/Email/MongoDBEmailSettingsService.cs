using CFEventHandler.Interfaces;
using MongoDB.Driver;

namespace CFEventHandler.Email
{
    public class MongoDBEmailSettingsService : IEmailSettingsService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<EmailEventSettings> _eventSettings;

        public MongoDBEmailSettingsService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventSettings = database.GetCollection<EmailEventSettings>("email_event_settings");
        }

        public async Task ImportAsync(IEntityList<EmailEventSettings> eventSettingsList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventSettings.InsertManyAsync(eventSettingsList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<EmailEventSettings> eventSettingsList)
        {
            eventSettingsList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<EmailEventSettings> GetAll()
        {
            return _eventSettings.Find(x => true).ToEnumerable();
        }

        public Task<EmailEventSettings?> GetByIdAsync(string id)
        {
            return _eventSettings.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<EmailEventSettings> AddAsync(EmailEventSettings eventSettings)
        {
            _eventSettings.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _eventSettings.DeleteManyAsync(Builders<EmailEventSettings>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventSettings.DeleteOneAsync(id);
        }
    }
}
