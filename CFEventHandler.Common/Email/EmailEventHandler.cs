using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System.Net.Mail;

namespace CFEventHandler.Email
{
    /// <summary>
    /// Handles event by sending email
    /// </summary>
    public class EmailEventHandler : IEventHandler
    {
        private readonly IDocumentTemplateProcessor _documentTemplateProcessor;
        private readonly IDocumentTemplateService _documentTemplateService;
        private readonly IEmailSettingsService _emailSettingsService;

        public string Id => "Email";

        public EmailEventHandler(IDocumentTemplateProcessor documentTemplateProcessor,
                                IDocumentTemplateService documentTemplateService,       
                                IEmailSettingsService emailSettingsService)
        {
            _documentTemplateProcessor = documentTemplateProcessor;
            _documentTemplateService = documentTemplateService;
            _emailSettingsService = emailSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {            
            // Get event settings
            var eventSettings = _emailSettingsService.GetByIdAsync(eventSettingsId).Result;

            System.Diagnostics.Debug.WriteLine($"Sending email to {eventSettings.RecipientAddresses[0]} for event {eventInstance.Id}");

            if (eventSettings != null && !String.IsNullOrEmpty(eventSettings.EmailConnection.Server))
            {
                // Create SMTP client
                using (var smtpClient = GetSmtpClient(eventSettings))
                {
                    // Create email
                    var mail = GetMailMessage(eventInstance, eventSettings);

                    // Send
                    smtpClient.Send(mail);
                }
            }
        }

        private MailMessage GetMailMessage(EventInstance eventInstance, EmailEventSettings emailEventSettings)
        {
            // Get document template for email content
            var documentTemplate = _documentTemplateService.GetByIdAsync(emailEventSettings.ContentDocumentTemplateId).Result;

            var mail = new MailMessage();
            mail.From = new MailAddress(emailEventSettings.SenderAddress);
            foreach(var address in emailEventSettings.RecipientAddresses)
            {
                mail.To.Add(address);
            }
            mail.IsBodyHtml = true;
            mail.Subject = eventInstance.EventTypeId;
            mail.Body = _documentTemplateProcessor.Process(documentTemplate, eventInstance).Result;            
            return mail;            
        }

        private static SmtpClient GetSmtpClient(EmailEventSettings eventSettings)
        {
            var smtpClient = new SmtpClient(eventSettings.EmailConnection.Server, eventSettings.EmailConnection.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(eventSettings.EmailConnection.Username, eventSettings.EmailConnection.Password);
            smtpClient.EnableSsl = true;
            return smtpClient;
        }
    }
}
