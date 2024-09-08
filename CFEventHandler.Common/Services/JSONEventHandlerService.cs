using CFEventHandler.Interfaces;
using CFEventHandler.JSON;
using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Services
{
    public class JSONEventHandlerService : JSONItemRepository<EventHandler, string>, IEventHandlerService
    {
        public JSONEventHandlerService(string folder) :
               base(folder,
                   ((EventHandler settings) => { return settings.Id; }),
                   ((EventHandler settings) => { settings.Id = Guid.NewGuid().ToString(); }))
        {

        }       
    }
}
