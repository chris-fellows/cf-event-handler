using CFEventHandler.Interfaces;
using CFEventHandler.SMS;

namespace CFEventHandler.Seed
{
    public class SMSEventSettingsSeed1 : IEntityReader<SMSEventSettings>
    {
        public async Task<List<SMSEventSettings>> ReadAllAsync()
        {
            var settings = new List<SMSEventSettings>();

            settings.Add(new SMSEventSettings()
            {
                //Id = "SMS1",
                Name = "SMS (Default)"
            });

            return settings;
        }
    }
}
