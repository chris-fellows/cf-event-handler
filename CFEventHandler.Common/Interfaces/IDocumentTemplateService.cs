using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IDocumentTemplateService
    {
        IEnumerable<DocumentTemplate> GetAll();

        Task<DocumentTemplate?> GetByIdAsync(string id);

        Task<DocumentTemplate> AddAsync(DocumentTemplate emailTemplate);

        Task DeleteAllAsync();

        Task DeleteByIdAsync(string id);
    }
}
