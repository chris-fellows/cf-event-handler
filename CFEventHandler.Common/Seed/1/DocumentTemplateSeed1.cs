using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Seed
{
    public class DocumentTemplateSeed1 : IEntityList<DocumentTemplate>
    {
        public async Task<List<DocumentTemplate>> ReadAllAsync()
        {
            var documentTemplates = new List<DocumentTemplate>();

            documentTemplates.Add(new DocumentTemplate()
            {
                Name = "Email template 1",
                Content = System.Text.Encoding.UTF8.GetBytes("<html>" +
                    "<head>" +
                    "</head>" +
                    "<body>" + 
                    "The following event was logged:<BR/>" +
                    "Id: \tEvent.Id\t<BR/>" +
                    "Event: \tEvent.EventTypeName\t<BR/>" +
                    "Client: \tEvent.EventClientName\t<BR/>" +
                    "Time: \tEvent.CreatedDateTime\t<BR/><BR/>" +
                    "Parameters:<BR/>" +
                    "\tEvent.Parameters\t" +
                    "</body>" +
                    "</html>")
            });

            documentTemplates.Add(new DocumentTemplate()
            {
                Name = "Email template 2",
                Content = System.Text.Encoding.UTF8.GetBytes("<html>" +
                  "<head>" +
                  "</head>" +
                  "<body>" +
                  "An event was logged" +
                  "</body>" +
                  "</html>")
            });

            return documentTemplates;
        }

        public async Task WriteAllAsync(List<DocumentTemplate> documentTemplates)
        {
            // No action
        }
    }
}
