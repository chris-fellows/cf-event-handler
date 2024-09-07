using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.HTTP
{
    /// <summary>
    /// Handles event by HTTP request
    /// </summary>
    public class HTTPEventHandler : IEventHandler
    {
        private readonly IHTTPSettingsService _httpSettingsService;

        public string Id => typeof(HTTPEventHandler).Name;

        public HTTPEventHandler(IHTTPSettingsService httpSettingsService)
        {
            _httpSettingsService = httpSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _httpSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
