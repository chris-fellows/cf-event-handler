using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IAPIKeyService
    {  
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ImportAsync(IEntityList<APIKeyInstance> eventTypeList);

        /// <summary>
        /// Exports to list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ExportAsync(IEntityList<APIKeyInstance> eventTypeList);

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns></returns>
        IEnumerable<APIKeyInstance> GetAll();

        /// <summary>
        /// Gets event type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<APIKeyInstance?> GetByIdAsync(string id);

        /// <summary>
        /// Gets API key by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<APIKeyInstance?> GetByNameAsync(string name);

        /// <summary>
        /// Adds event type
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<APIKeyInstance> AddAsync(APIKeyInstance eventType);

        /// <summary>
        /// Deletes all API keys
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes API key by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);
    }
}
