using CFEventHandler.EventHandlers;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Handle(EventInstance eventInstance)
        {

        }
    }
}
