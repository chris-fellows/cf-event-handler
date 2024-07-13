using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Console
{
    public class ConsoleSettingsService : IConsoleSettingsService
    {               
        public ConsoleEventSettings GetSettings(EventInstance eventInstance)
        {
            return new ConsoleEventSettings();
        }
    }
}
