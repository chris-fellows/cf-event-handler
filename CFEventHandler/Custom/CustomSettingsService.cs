using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Custom
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public CustomEventSettings GetSettings(EventInstance eventInstance)
        {
            return new CustomEventSettings();
        }
    }
}
