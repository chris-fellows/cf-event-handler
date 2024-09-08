using CFEventHandler.Common.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class EventService : IEventService
    { 
        public async Task<EventInstance> AddAsync(EventInstance eventInstance)
        {
            return null;
        }
        
        public Task<List<EventInstance>> GetByFilter(EventFilter eventFilter)
        {
            return null;
        }
    }
}
