using CFEventHandler.Interfaces;
using CFEventHandler.SignalR;

namespace CFEventHandler.Seed
{
    public class SignalREventSettingsSeed1 : IEntityList<SignalREventSettings>
    {
        public async Task<List<SignalREventSettings>> ReadAllAsync()
        {
            var settings = new List<SignalREventSettings>();

            settings.Add(new SignalREventSettings()
            {
                //Id = "SMS1",
                Name = "SignalR (Default)"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<SignalREventSettings> settingsList)
        {
            // No action
        }
    }
}
