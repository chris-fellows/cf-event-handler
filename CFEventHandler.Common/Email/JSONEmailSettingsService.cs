using CFEventHandler.Custom;
using CFEventHandler.Interfaces;
using CFEventHandler.JSON;
using CFEventHandler.Models;
using System.Collections.Generic;

namespace CFEventHandler.Email
{
    public class JSONEmailSettingsService : JSONItemRepository<EmailEventSettings, string>, IEmailSettingsService
    {
        public JSONEmailSettingsService(string folder) :
                       base(folder,
                           ((EmailEventSettings settings) => { return settings.Id; }),
                           ((EmailEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
