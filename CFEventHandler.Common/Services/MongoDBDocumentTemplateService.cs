using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBDocumentTemplateService : IDocumentTemplateService
    {
        private readonly MongoClient? _client;
        private readonly IMongoCollection<DocumentTemplate> _documentTemplates;

        public MongoDBDocumentTemplateService(ITenantDatabaseConfig databaseConfig)
        {
            _client = new MongoClient(databaseConfig.ConnectionString);
            var database = _client.GetDatabase(databaseConfig.DatabaseName);
            try
            {
                _documentTemplates = database.GetCollection<DocumentTemplate>("document_templates");
                int xxx = 1000;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task ImportAsync(IEntityList<DocumentTemplate> documentTemplateList)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                await _documentTemplates.InsertManyAsync(documentTemplateList.ReadAllAsync().Result);
                await session.CommitTransactionAsync();
            }
        }

        public Task ExportAsync(IEntityList<DocumentTemplate> documentTemplateList)
        {
            documentTemplateList.WriteAllAsync(GetAll().ToList());
            return Task.CompletedTask;
        }

        public IEnumerable<DocumentTemplate> GetAll()
        {
            return _documentTemplates.Find(x => true).ToEnumerable();
        }

        public Task<DocumentTemplate?> GetByIdAsync(string id)
        {
            return _documentTemplates.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<DocumentTemplate> AddAsync(DocumentTemplate eventSettings)
        {
            _documentTemplates.InsertOneAsync(eventSettings);
            return Task.FromResult(eventSettings);
        }

        public async Task DeleteAllAsync()
        {
            await _documentTemplates.DeleteManyAsync(Builders<DocumentTemplate>.Filter.Empty);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _documentTemplates.DeleteOneAsync(id);
        }
    }
}
