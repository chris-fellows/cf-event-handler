using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Memory event queue service
    /// </summary>
    public class MemoryEventQueueService : IEventQueueService
    {
        private readonly Queue<EventInstance> _eventInstances = new Queue<EventInstance>();

        public void Add(EventInstance eventInstance)
        {
            _eventInstances.Enqueue(eventInstance);
        }

        public int Count => _eventInstances.Count;

        public EventInstance? GetNext()
        {
            if (_eventInstances.Count == 0) return null;
            return _eventInstances.Dequeue();
        }
    }
}
