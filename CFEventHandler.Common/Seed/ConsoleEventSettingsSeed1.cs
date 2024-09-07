using CFEventHandler.Console;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Seed
{
    public class ConsoleEventSettingsSeed1 : IEntityList<ConsoleEventSettings>
    {
        public async Task<List<ConsoleEventSettings>> ReadAllAsync()
        {
            var settings = new List<ConsoleEventSettings>();

            settings.Add(new ConsoleEventSettings()
            {
                Id = "1"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<ConsoleEventSettings> settingsList)
        {
            // No action
        }
    }
}
