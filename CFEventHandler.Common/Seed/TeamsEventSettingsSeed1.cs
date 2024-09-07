using CFEventHandler.Interfaces;
using CFEventHandler.Teams;

namespace CFEventHandler.Common.Seed
{
    public class TeamsEventSettingsSeed1 : IEntityList<TeamsEventSettings>
    {
        public async Task<List<TeamsEventSettings>> ReadAllAsync()
        {
            var settings = new List<TeamsEventSettings>();

            settings.Add(new TeamsEventSettings()
            {
                Id = "1",
                URL = ""
            });

            return settings;
        }

        public async Task WriteAllAsync(List<TeamsEventSettings> settingsList)
        {
            // No action
        }
    }
}
