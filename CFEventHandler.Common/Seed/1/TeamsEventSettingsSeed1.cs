using CFEventHandler.Interfaces;
using CFEventHandler.Teams;

namespace CFEventHandler.Seed
{
    public class TeamsEventSettingsSeed1 : IEntityList<TeamsEventSettings>
    {
        public async Task<List<TeamsEventSettings>> ReadAllAsync()
        {
            var settings = new List<TeamsEventSettings>();

            settings.Add(new TeamsEventSettings()
            {
                Id = "Teams1",
                Name = "Teams (Default)",
                URL = "https://www.google.co.uk"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<TeamsEventSettings> settingsList)
        {
            // No action
        }
    }
}
