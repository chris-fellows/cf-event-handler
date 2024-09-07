using CFEventHandler.Interfaces;
using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        public async Task Import(IEntityList<EventHandler> eventHandlerList)
        {

        }

        public async Task Export(IEntityList<EventHandler> eventHandlerList)
        {

        }

        public async Task<List<EventHandler>> GetAllAsync()
        {
            return new List<EventHandler>();
        }

        public async Task<EventHandler> GetByIdAsync(string id)
        {
            return new EventHandler();
        }

        public async Task<EventHandler> AddAsync(EventHandler eventHandler)
        {
            return eventHandler;
        }

        public async Task DeleteByIdAsync(string id)
        {

        }

        public async Task DeleteAllAsync()
        {

        }
    }
}
