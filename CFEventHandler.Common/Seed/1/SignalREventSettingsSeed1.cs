using CFEventHandler.Interfaces;
using CFEventHandler.SignalR;

namespace CFEventHandler.Seed
{
    public class SignalREventSettingsSeed1 : IEntityReader<SignalREventSettings>
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
    }
}
