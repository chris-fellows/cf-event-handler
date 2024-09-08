using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IEventManagerService
    {
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="eventInstance"></param>
        void Handle(EventInstance eventInstance);
    }
}
