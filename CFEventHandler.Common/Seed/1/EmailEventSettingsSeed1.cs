using CFEventHandler.Email;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Seed
{
    public class EmailEventSettingsSeed1 : IEntityList<EmailEventSettings>
    {
        public async Task<List<EmailEventSettings>> ReadAllAsync()
        {
            var settings = new List<EmailEventSettings>();

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
            });

            return settings;
        }

        public async Task WriteAllAsync(List<EmailEventSettings> settingsList)
        {
            // No action
        }
    }
}
