using CFEventHandler.Interfaces;
using CFEventHandler.JSON;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class JSONEventHandlerRuleService : JSONItemRepository<EventHandlerRule, string>, IEventHandlerRuleService
    {
        public JSONEventHandlerRuleService(string folder) :
               base(folder,
                   ((EventHandlerRule eventHandlerRule) => { return eventHandlerRule.Id; }),
                   ((EventHandlerRule eventHandlerRule) => { eventHandlerRule.Id = Guid.NewGuid().ToString(); }))
        {

        }
    }
}
