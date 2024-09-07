using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Event handler rule service
    /// </summary>
    public interface IEventHandlerRuleService
    {
        Task<List<EventHandlerRule>> GetAllAsync();
    }
}
