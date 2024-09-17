using CFEventHandler.Enums;

namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event handler DTO
    /// </summary>
    public class EventHandlerDTO
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public EventHandlerTypes EventHandlerType { get; set; }
    }
}
