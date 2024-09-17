using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Event instance service. Typically saves event for reporting.
    /// </summary>
    public interface IEventService
    {       
        /// <summary>
        /// Gets event by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EventInstance?> GetByIdAsync(string id);

        /// <summary>
        /// Adds event
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventInstance> AddAsync(EventInstance eventInstance);

        /// <summary>
        /// Deletes all event events
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes event by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);

        /// <summary>
        /// Gets events by filter
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        Task<List<EventInstance>> GetByFilter(EventFilter eventFilter);
    }
}
