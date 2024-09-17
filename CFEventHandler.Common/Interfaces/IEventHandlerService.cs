using CFEventHandler.Models;
using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Event handler service
    /// </summary>
    public interface IEventHandlerService
    {
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventHandlerList"></param>
        /// <returns></returns>
        Task ImportAsync(IEntityList<EventHandler> eventHandlerList);

        /// <summary>
        /// Exports to list
        /// </summary>
        /// <param name="eventHandlerList"></param>
        /// <returns></returns>
        Task ExportAsync(IEntityList<EventHandler> eventHandlerList);

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns></returns>
        IEnumerable<EventHandler> GetAll();

        /// <summary>
        /// Gets event handler by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EventHandler?> GetByIdAsync(string id);

        /// <summary>
        /// Gets event handler byname
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<EventHandler?> GetByNameAsync(string name);
        
        /// <summary>
        /// Adds event handler
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventHandler> AddAsync(EventHandler eventType);

        /// <summary>
        /// Deletes all event handlers
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes event handler by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);
    }
}
