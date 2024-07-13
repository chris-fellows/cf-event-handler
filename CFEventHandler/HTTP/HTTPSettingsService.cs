using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.HTTP
{
    public class HTTPSettingsService : IHTTPSettingsService
    {
        public HTTPEventSettings GetSettings(EventInstance eventInstance)
        {
            return new HTTPEventSettings();
        }
    }
}
