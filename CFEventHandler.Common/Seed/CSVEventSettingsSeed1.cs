using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Common.Seed
{
    public class CSVEventSettingsSeed1 : IEntityList<CSVEventSettings>
    {
        public async Task<List<CSVEventSettings>> ReadAllAsync()
        {
            var settings = new List<CSVEventSettings>();

            settings.Add(new CSVEventSettings()
            {
                Id = "CSV1",
                Name = "CSV (Default)"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<CSVEventSettings> settingsList)
        {
            // No action
        }
    }
}
