using CFEventHandler.Enums;

namespace CFEventHandler.Models.DTO
{
    public class EventHandlerDTO
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public EventHandlerTypes EventHandlerType { get; set; }
    }
}
