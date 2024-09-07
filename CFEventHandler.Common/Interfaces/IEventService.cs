using CFEventHandler.Models;

namespace CFEventHandler.Common.Interfaces
{
    /// <summary>
    /// Event instance service. Typically saves event for reporting.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Adds event
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventInstance> AddAsync(EventInstance eventInstance);

        /// <summary>
        /// Gets events by filter
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        Task<List<EventInstance>> GetByFilter(EventFilter eventFilter);
    }
}
