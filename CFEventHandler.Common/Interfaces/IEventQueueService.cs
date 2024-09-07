using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IEventQueueService
    {
        void Add(EventInstance eventInstance);

        int Count { get; }

        EventInstance? GetNext();
    }
}
