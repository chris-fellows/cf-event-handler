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
                Id = "Email1",
                Name = "Email (Default)",
                Password = "",
                Port = 587,
                RecipientAddresses = new List<string>() { "chrismfellows@hotmail.co.uk" },
                SenderAddress = "chrismfellows@hotmail.co.uk",
                Server = "smtp.live.com",
                Username = "chrismfellows@hotmail.co.uk"                
            });

            settings.Add(new EmailEventSettings()
            {
                Id = "Email2",
                Name = "Email (2)"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<EmailEventSettings> settingsList)
        {
            // No action
        }
    }
}
