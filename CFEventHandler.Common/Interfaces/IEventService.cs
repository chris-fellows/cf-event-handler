using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Event instance service. Typically saves event for reporting.
    /// </summary>
    public interface IEventService : IEntityWithIDService<EventInstance, string>
    {            
        /// <summary>
        /// Gets events by filter
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        Task<List<EventInstance>> GetByFilter(EventFilter eventFilter);

        /// <summary>
        /// Deletes events by filter
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        Task DeleteByFilter(EventFilter eventFilter);

        /// <summary>
        /// Gets event summary
        /// </summary>
        /// <param name="eventFilter"></param>
        /// <returns></returns>
        Task<List<EventSummary>> GetEventSummary(EventFilter eventFilter);
    }
}
