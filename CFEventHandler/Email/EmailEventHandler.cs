using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace CFEventHandler.Email
{
    /// <summary>
    /// Handles event by sending email
    /// </summary>
    public class EmailEventHandler : IEventHandler
    {        
        private readonly IEnumerable<IEmailCreator> _emailFormatters;
        private readonly IEmailSettingsService _emailSettingsService;

        public string Id => typeof(EmailEventHandler).Name;

        public EmailEventHandler(IEnumerable<IEmailCreator> emailFormatters,
                                IEmailSettingsService emailSettingsService)
        {
            _emailFormatters = emailFormatters;
            _emailSettingsService = emailSettingsService;
        }

        public void Handle(EventInstance eventInstance)
        {
            // Get event settings
            var eventSettings = _emailSettingsService.GetSettings(eventInstance);

            if (eventSettings != null && !String.IsNullOrEmpty(eventSettings.Server))
            {
                // Create SMTP client
                using (var smtpClient = GetSmtpClient(eventSettings))
                {
                    // Get email creator                    
                    var emailCreator = _emailFormatters.FirstOrDefault(ef => ef.Id.Equals(eventSettings.EmailCreatorId, StringComparison.InvariantCultureIgnoreCase));

                    // Create email
                    var email = emailCreator.Create(eventInstance, eventSettings);
                    smtpClient.Send(email);
                }
            }            
        }
    
        private SmtpClient GetSmtpClient(EmailEventSettings eventSettings)
        {
            var smtpClient = new SmtpClient(eventSettings.Server, eventSettings.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(eventSettings.Username, eventSettings.Password);
            smtpClient.EnableSsl = true;
            return smtpClient;
        }
    }
}
