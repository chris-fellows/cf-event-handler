using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Processes document template
    /// </summary>
    public interface IDocumentTemplateProcessor
    {
        Task<string> Process(DocumentTemplate documentTemplate, EventInstance eventInstance);
    }
}
