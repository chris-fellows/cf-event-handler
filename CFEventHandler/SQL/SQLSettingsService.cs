using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.SQL
{
    public class SQLSettingsService : ISQLSettingsService
    {
        public SQLEventSettings GetSettings(EventInstance eventInstance)
        {
            return new SQLEventSettings();
        }
    }
}
