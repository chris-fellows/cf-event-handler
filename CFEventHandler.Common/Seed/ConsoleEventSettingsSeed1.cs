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
                Id = "Console1",
                Name = "Console (Default)"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<ConsoleEventSettings> settingsList)
        {
            // No action
        }
    }
}
