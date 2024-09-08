using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.JSON;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Custom
{
    public class JSONCustomSettingsService : JSONItemRepository<CustomEventSettings, string>, ICustomSettingsService
    {
        public JSONCustomSettingsService(string folder) :
                     base(folder,
                         ((CustomEventSettings settings) => { return settings.Id; }),
                         ((CustomEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
