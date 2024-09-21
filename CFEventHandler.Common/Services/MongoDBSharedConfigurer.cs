using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Configures MongoDB shared database
    /// </summary>
    public class MongoDBSharedConfigurer : ISharedDatabaseConfigurer
    {       
        private readonly IDatabaseConfig _databaseConfig;        

        private readonly IServiceProvider _serviceProvider;

        public MongoDBSharedConfigurer(IDatabaseConfig databaseConfig,
                        IServiceProvider serviceProvider)                        
        {            
            _databaseConfig = databaseConfig;
            _serviceProvider = serviceProvider;            
        }

        public async Task InitialiseAsync()
        {
            // Configure main DB
            var client = new MongoClient(_databaseConfig.ConnectionString);
            var database = client.GetDatabase(_databaseConfig.DatabaseName);

            await InitialiseTenants(database);          
        }    

        private async Task InitialiseTenants(IMongoDatabase database)
        {
            var collection = database.GetCollection<Tenant>("tenants");
            var indexDefinitionName = Builders<Tenant>.IndexKeys.Ascending(x => x.Name);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<Tenant>(indexDefinitionName));
        }
    }
}
