using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IDocumentTemplateService
    {        
        Task ImportAsync(IEntityList<DocumentTemplate> documentTemplateList);
        
        Task ExportAsync(IEntityList<DocumentTemplate> documentTemplateList);

        IEnumerable<DocumentTemplate> GetAll();

        Task<DocumentTemplate?> GetByIdAsync(string id);

        Task<DocumentTemplate> AddAsync(DocumentTemplate emailTemplate);

        Task DeleteAllAsync();

        Task DeleteByIdAsync(string id);
    }
}
