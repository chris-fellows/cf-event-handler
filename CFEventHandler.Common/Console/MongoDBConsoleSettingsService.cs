using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Console
{
    public class MongoDBConsoleSettingsService : IConsoleSettingsService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<ConsoleEventSettings> _eventSettings;

        public MongoDBConsoleSettingsService(IDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _eventSettings = database.GetCollection<ConsoleEventSettings>("console_event_settings");
        }

        public async Task ImportAsync(IEntityList<ConsoleEventSettings> eventSettingsList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _eventSettings.InsertManyAsync(eventSettingsList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<ConsoleEventSettings> eventSettingsList)
        {
            eventSettingsList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<ConsoleEventSettings> GetAll()
        {
            return _eventSettings.Find(x => true).ToEnumerable();
        }

        public Task<ConsoleEventSettings?> GetByIdAsync(string id)
        {
            return _eventSettings.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<ConsoleEventSettings> AddAsync(ConsoleEventSettings eventSettings)
        {
            _eventSettings.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _eventSettings.DeleteManyAsync(Builders<ConsoleEventSettings>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _eventSettings.DeleteOneAsync(id);
        }
    }
}
