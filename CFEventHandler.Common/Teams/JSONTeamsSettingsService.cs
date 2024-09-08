using CFEventHandler.JSON;
using CFEventHandler.SQL;

namespace CFEventHandler.Teams
{
    public class JSONTeamsSettingsService : JSONItemRepository<TeamsEventSettings, string>, ITeamsSettingsService
    {
        public JSONTeamsSettingsService(string folder) :
                      base(folder,
                          ((TeamsEventSettings settings) => { return settings.Id; }),
                          ((TeamsEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
