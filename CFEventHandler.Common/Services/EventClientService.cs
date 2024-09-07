using CFEventHandler.Common.Interfaces;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Common.Services
{
    /// <summary>
    /// Service for event client instances
    /// </summary>
    public class EventClientService : IEventClientService
    {
        public async Task Import(IEntityList<EventClient> eventClientList)
        {

        }

        public async Task Export(IEntityList<EventClient> eventClientList)
        {

        }

        public async Task<List<EventClient>> GetAllAsync()
        {
            return new List<EventClient>();
        }

        public async Task<EventClient> GetByIdAsync(string id)
        {
            return new EventClient();
        }

        public async Task<EventClient> AddAsync(EventClient eventClient)
        {
            return eventClient;
        }

        public async Task DeleteByIdAsync(string id)
        {

        }

        public async Task DeleteAllAsync()
        {

        }
    }
}
