using CFEventHandler.Models;
using CFEventHandler.Teams;
using MongoDB.Driver;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Event handler rule service
    /// </summary>
    public interface IEventHandlerRuleService
    {
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ImportAsync(IEntityList<EventHandlerRule> eventHandlerRuleList);

        IEnumerable<EventHandlerRule> GetAll();

        Task DeleteAllAsync();        
    }
}
