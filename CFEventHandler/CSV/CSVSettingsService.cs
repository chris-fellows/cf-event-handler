using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.CSV
{
    public class CSVSettingsService : ICSVSettingsService
    {
        public CSVEventSettings GetSettings(EventInstance eventInstance)
        {
            return new CSVEventSettings();
        }
    }
}
