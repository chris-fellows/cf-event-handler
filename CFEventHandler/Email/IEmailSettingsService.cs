using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Email
{
    public interface IEmailSettingsService
    {
        EmailEventSettings GetSettings(EventInstance eventInstance);
    }
}
