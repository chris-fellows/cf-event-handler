using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.CSV
{
    /// <summary>
    /// Handles event by logging to CSV
    /// </summary>
    public class CSVEventHandler : IEventHandler
    {
        private readonly ICSVSettingsService _csvSettingsService;

        public string Id => typeof(CSVEventHandler).Name;

        public CSVEventHandler(ICSVSettingsService csvSettingsService)
        {
            _csvSettingsService = csvSettingsService;
        }

        public void Handle(EventInstance eventInstance)
        {

        }
    }
}
