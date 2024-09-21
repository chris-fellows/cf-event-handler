using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;

namespace CFEventHandler.Services
{
    public class MongoDBDocumentTemplateService : MongoDBBaseService<DocumentTemplate>, IDocumentTemplateService
    {        
        public MongoDBDocumentTemplateService(ITenantDatabaseConfig databaseConfig) : base(databaseConfig, "document_templates")
        {
        
        }
    
        public Task<DocumentTemplate?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<DocumentTemplate?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
