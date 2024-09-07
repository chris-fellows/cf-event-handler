using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Common.Services
{
    public class EventHandlerRuleService : IEventHandlerRuleService
    {
        public async Task<List<EventHandlerRule>> GetAllAsync()
        {
            return new List<EventHandlerRule>();
        }
    }
}
