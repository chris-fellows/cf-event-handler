using CFEventHandler.Interfaces;
using CFEventHandler.JSON;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class JSONEventTypeService : JSONItemRepository<EventType, string>, IEventTypeService
    {
        public JSONEventTypeService(string folder) :
                   base(folder,
                       ((EventType eventType) => { return eventType.Id; }),
                       ((EventType eventType) => { eventType.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
