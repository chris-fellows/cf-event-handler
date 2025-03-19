using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Seed
{
    public class EventClientSeed1 : IEntityReader<EventClient>
    {
        public async Task<List<EventClient>> ReadAllAsync()
        {
            var eventClients = new List<EventClient>();

            eventClients.Add(new EventClient()
            {
                //Id = "1",
                Name = "Client 1"
            });

            eventClients.Add(new EventClient()
            {
                //Id = "2",
                Name = "Client 2"
            });

            return eventClients;
        }
    }
}
