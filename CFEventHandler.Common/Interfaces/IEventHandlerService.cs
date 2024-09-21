using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Manages EventHandler instances
    /// </summary>
    public interface IEventHandlerService : IEntityWithIDService<EventHandler, string>
    {
      
    }
}
