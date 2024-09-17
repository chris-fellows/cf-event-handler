using CFEventHandler.Interfaces;
using CFEventHandler.SQL;

namespace CFEventHandler.Seed
{
    public class SQLEventSettingsSeed1 : IEntityList<SQLEventSettings>
    {
        public async Task<List<SQLEventSettings>> ReadAllAsync()
        {
            var settings = new List<SQLEventSettings>();

            settings.Add(new SQLEventSettings()
            {
                //Id = "SQL1",
                Name = "SQL (Default)",
                ConnectionString = "Test"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<SQLEventSettings> settingsList)
        {
            // No action
        }
    }
}
