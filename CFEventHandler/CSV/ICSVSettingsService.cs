using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.CSV
{
    public interface ICSVSettingsService
    {
        CSVEventSettings GetSettings(EventInstance eventInstance);
    }
}
