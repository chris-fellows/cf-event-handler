using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class DocumentTemplateProcessor : IDocumentTemplateProcessor
    {
        public Task<string> Process(DocumentTemplate documentTemplate, EventInstance eventInstance)
        {
            var content = System.Text.Encoding.UTF8.GetString(documentTemplate.Content);

            // TODO: Get event client event type
            EventClient eventClient = null;
            EventType eventType = null;

            content = content.Replace("\tEvent.Id\t", eventInstance.Id)
                    .Replace("\tEvent.EventClientName\t", eventClient.Name)
                    .Replace("\tEvent.EventTypeName\t", eventType.Name);

            // Replace placeholders for event parameters
            foreach(var parameter in eventInstance.Parameters)
            {
                var placeholder = $"\tEvent.Parameter.{parameter.Name}\t";
                var value = parameter.Value.ToString();
                content = content.Replace(placeholder, value);
            }

            return Task.FromResult(content);
        }
    }
}
