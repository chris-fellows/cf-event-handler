using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Teams
{
    public class TeamsSettingsService : ITeamsSettingsService
    {
        public TeamsEventSettings GetSettings(EventInstance eventInstance)
        {
            return new TeamsEventSettings();
        }
    }
}
