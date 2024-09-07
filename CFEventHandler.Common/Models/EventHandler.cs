using CFEventHandler.Enums;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Event handler
    /// </summary>
    public class EventHandler
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public EventHandlerTypes EventHandlerType { get; set; }
    }
}
