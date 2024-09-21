using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Queue for EventInstance instances
    /// </summary>
    public interface IEventQueueService
    {
        void Add(EventInstance eventInstance);

        int Count { get; }

        EventInstance? GetNext();
    }
}
