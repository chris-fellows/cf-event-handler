using CFEventHandler.Email;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Common.Seed
{
    public class EmailEventSettingsSeed1 : IEntityList<EmailEventSettings>
    {
        public async Task<List<EmailEventSettings>> ReadAllAsync()
        {
            var settings = new List<EmailEventSettings>();

            return settings;
        }

        public async Task WriteAllAsync(List<EmailEventSettings> settingsList)
        {
            // No action
        }
    }
}
