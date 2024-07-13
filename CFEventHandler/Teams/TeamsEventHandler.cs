using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Teams
{

    /// <summary>
    /// Handles event by logging to Teams
    /// </summary>
    public class TeamsEventHandler : IEventHandler
    {
        private readonly ITeamsSettingsService _teamsSettingsService;
            
        public string Id => typeof(TeamsEventHandler).Name;

        public TeamsEventHandler(ITeamsSettingsService teamsSettingsService)
        {
            _teamsSettingsService = teamsSettingsService;
        }

        public void Handle(EventInstance eventInstance)
        {

        }
    }
}
