using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class DocumentTemplateProcessor : IDocumentTemplateProcessor
    {
        public Task<string> Process(DocumentTemplate documentTemplate, EventInstance eventInstance)
        {
            var content = System.Text.Encoding.UTF8.GetString(documentTemplate.Content);

            // Replace placeholders for event parameters
            foreach(var parameter in eventInstance.Parameters)
            {
                var placeholder = $"\tEventParameter:{parameter.Name}\t";
                var value = parameter.Value.ToString();
                content = content.Replace(placeholder, value);
            }

            return Task.FromResult(content);
        }
    }
}
