using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Seed
{
    /// <summary>
    /// Event type seed example 1
    /// </summary>
    public class EventTypeSeed1 : IEntityList<EventType>
    {
        public async Task<List<EventType>> ReadAllAsync()
        {
            var eventTypes = new List<EventType>();

            eventTypes.Add(new EventType()
            {
                Id = "1",
                Name = "Test event 1"
            });

            eventTypes.Add(new EventType()
            {
                Id = "2",
                Name = "Test event 2"
            });

            eventTypes.Add(new EventType()
            {
                Id = "3",
                Name = "Test event 3"
            });

            eventTypes.Add(new EventType()
            {
                Id = "4",
                Name = "Test event 4"
            });

            eventTypes.Add(new EventType()
            {
                Id = "5",
                Name = "Test event 5"
            });

            return eventTypes;
        }

        public async Task WriteAllAsync(List<EventType> eventTypes)
        {
            // No action
        }
    }
}
