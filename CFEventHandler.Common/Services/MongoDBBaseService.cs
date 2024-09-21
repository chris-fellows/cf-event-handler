using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Base service for managing entities in MongoDB
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public abstract class MongoDBBaseService<TEntityType>
    {
        protected MongoClient? _client;
        protected IMongoCollection<TEntityType> _entities;

        public MongoDBBaseService(IDatabaseConfig databaseConfig, string collectionName)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            _entities = database.GetCollection<TEntityType>(collectionName);
        }

        public async Task ImportAsync(IEntityList<TEntityType> entityList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    await _entities.InsertManyAsync(entityList.ReadAllAsync().Result);
                    await session.CommitTransactionAsync();
                }
                catch(Exception exception)
                {
                    await session.AbortTransactionAsync();
                    throw exception;
                }
            }
        }

        public Task ExportAsync(IEntityList<TEntityType> eventTypeList)
        {
            eventTypeList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<TEntityType> GetAll()
        {
            return _entities.Find(x => true).ToEnumerable();
        }

        //public Task<TEntityType?> GetByIdAsync(string id)
        //{
        //    return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public Task<TEntityType?> GetByNameAsync(string name)
        //{
        //    return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        //}

        public Task<TEntityType> AddAsync(TEntityType eventType)
        {
            _entities.InsertOneAsync(eventType);
            return Task.FromResult(eventType);
        }

        public async Task DeleteAllAsync()
        {
            await _entities.DeleteManyAsync(Builders<TEntityType>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
