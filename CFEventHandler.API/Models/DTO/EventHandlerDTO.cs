using CFEventHandler.Enums;
using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event handler DTO
    /// </summary>
    public class EventHandlerDTO
    {
        public string Id { get; set; } = String.Empty;

        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;

        public EventHandlerTypes EventHandlerType { get; set; }
    }
}
