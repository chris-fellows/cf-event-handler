using CFEventHandler.Email;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Seed
{
    public class EmailEventSettingsSeed1 : IEntityList<EmailEventSettings>
    {
        private readonly IDocumentTemplateService _documentTemplateService;

        public EmailEventSettingsSeed1(IDocumentTemplateService documentTemplateService)
        {
            _documentTemplateService = documentTemplateService;
        }

        public async Task<List<EmailEventSettings>> ReadAllAsync()
        {
            var settings = new List<EmailEventSettings>();

            var documentTemplates = _documentTemplateService.GetAll();
            var documentTemplateEmail1 = documentTemplates.FirstOrDefault(dt => dt.Name == "Email template 1");
            var documentTemplateEmail2 = documentTemplates.FirstOrDefault(dt => dt.Name == "Email template 2");

            settings.Add(new EmailEventSettings()
            {             
                Name = "Email (Default)",                
                EmailConnection = new EmailConnection()
                {
                    Password = "",
                    Port = 587,
                    Server = "smtp.live.com",
                    Username = "chrismfellows@hotmail.co.uk"
                },
                RecipientAddresses = new List<string>() { "chrismfellows@hotmail.co.uk" },
                SenderAddress = "chrismfellows@hotmail.co.uk",
                ContentDocumentTemplateId = documentTemplateEmail1.Id
            });

            settings.Add(new EmailEventSettings()
            {
                //Id = "Email2",
                Name = "Email (2)",
                EmailConnection = new EmailConnection()
                {
                    Password = "",
                    Port = 587,
                    Server = "smtp.live.com",
                    Username = "chrismfellows@hotmail.co.uk"
                },
                RecipientAddresses = new List<string>() { "chrismfellows@xxxxxxxxx.co.uk" },
                SenderAddress = "chrismfellows@xxxxxxxxx.co.uk",
                ContentDocumentTemplateId = documentTemplateEmail2.Id
            });

            return settings;
        }

        public async Task WriteAllAsync(List<EmailEventSettings> settingsList)
        {
            // No action
        }
    }
}
