using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Common.Seed
{
    public class HTTPEventSettingsSeed1 : IEntityList<HTTPEventSettings>
    {
        public async Task<List<HTTPEventSettings>> ReadAllAsync()
        {
            var settings = new List<HTTPEventSettings>();

            settings.Add(new HTTPEventSettings()
            {
                Id = "HTTP1",
                Method = "POST",
                Headers = new Dictionary<string, string>(),
                URL = "http://myapi/dosomething"
            });
            
            return settings;
        }

        public async Task WriteAllAsync(List<HTTPEventSettings> settingsList)
        {
            // No action
        }
    }
}
