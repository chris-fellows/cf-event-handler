using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;

namespace CFEventHandler.Seed
{
    public class HTTPEventSettingsSeed1 : IEntityReader<HTTPEventSettings>
    {
        public async Task<List<HTTPEventSettings>> ReadAllAsync()
        {
            var settings = new List<HTTPEventSettings>();

            settings.Add(new HTTPEventSettings()
            {
                //Id = "HTTP1",
                Name = "HTTP (Default)",
                Method = "POST",
                Headers = new Dictionary<string, string>(),
                URL = "http://myapi/dosomething"
            });
            
            return settings;
        }
    }
}
