using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public  interface IEventTypeService
    {
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task Import(IEntityList<EventType> eventTypeList);

        /// <summary>
        /// Exports to list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task Export(IEntityList<EventType> eventTypeList);

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns></returns>
        Task<List<EventType>> GetAllAsync();

        /// <summary>
        /// Gets event type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EventType> GetByIdAsync(string id);

        /// <summary>
        /// Adds event type
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventType> AddAsync(EventType eventType);

        /// <summary>
        /// Deletes all event types
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes event type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);
    }
}
