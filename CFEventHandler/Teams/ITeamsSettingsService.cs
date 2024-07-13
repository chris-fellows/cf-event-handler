using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Teams
{
    public interface ITeamsSettingsService
    {
        TeamsEventSettings GetSettings(EventInstance eventInstance);
    }
}
