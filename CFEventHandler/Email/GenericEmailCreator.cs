using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Email
{
    /// <summary>
    /// Generic email creator. This is really only for test purposes.
    /// </summary>
    public class GenericEmailCreator : IEmailCreator
    {
        public string Id => typeof(GenericEmailCreator).Name;

        public MailMessage Create(EventInstance eventInstance, EmailEventSettings eventSettings)
        {
            var mailMessage = new MailMessage()
            {
                Subject = "Test message",
                From = new MailAddress(eventSettings.SenderAddress),                
                IsBodyHtml = true,
                Body = "This is a test message"                 
            };
            foreach(var address in eventSettings.RecipientAddresses)
            {
                mailMessage.To.Add(address);
            }
          
            return mailMessage;
        }
    }
}
