using CFEventHandler.CSV;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Seed
{
    public class CSVEventSettingsSeed1 : IEntityReader<CSVEventSettings>
    {
        public async Task<List<CSVEventSettings>> ReadAllAsync()
        {
            var settings = new List<CSVEventSettings>();

            settings.Add(new CSVEventSettings()
            {
                //Id = "CSV1",
                Name = "CSV (Default)"
            });

            return settings;
        }
    }
}
