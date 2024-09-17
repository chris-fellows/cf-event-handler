using CFEventHandler.Models;

namespace CFEventHandler.Email
{
    /// <summary>
    /// Settings for handling event for sending email
    /// </summary>
    public class EmailEventSettings : EventSettings
    {
        /// <summary>
        /// Email connection details
        /// </summary>
        public EmailConnection EmailConnection { get; set; }
       
        public string SenderAddress { get; set; } = String.Empty;

        public List<string> RecipientAddresses { get; set; }

        /// <summary>
        /// Document template for email content (DocumentTemplate.Id)
        /// </summary>
        public string ContentDocumentTemplateId { get; set; } = String.Empty;
    }
}
