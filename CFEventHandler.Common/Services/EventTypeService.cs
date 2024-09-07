using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class EventTypeService : IEventTypeService
    {        
        public async Task Import(IEntityList<EventType> eventTypeList)
        {

        }

        public async Task Export(IEntityList<EventType> eventTypeList)
        {

        }

        public async Task<List<EventType>> GetAllAsync()
        {
            return new List<EventType>();
        }
        
        public async Task<EventType> GetByIdAsync(string id)
        {
            return new EventType();
        }

        public async Task<EventType> AddAsync(EventType eventType)
        {
            return eventType;
        }

        public async Task DeleteByIdAsync(string id)
        {

        }

        public async Task DeleteAllAsync()
        {

        }
    }
}
