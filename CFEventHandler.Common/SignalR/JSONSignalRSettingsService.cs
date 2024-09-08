using CFEventHandler.JSON;
using CFEventHandler.Process;

namespace CFEventHandler.SignalR
{
    public class JSONSignalRSettingsService : JSONItemRepository<SignalREventSettings, string>, ISignalRSettingsService
    {
        public JSONSignalRSettingsService(string folder) :
                      base(folder,
                          ((SignalREventSettings settings) => { return settings.Id; }),
                          ((SignalREventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
