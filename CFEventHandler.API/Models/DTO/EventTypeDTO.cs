using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event type DTO
    /// </summary>
    public class EventTypeDTO
    {
        public string Id { get; set; } = String.Empty;

        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;
    }
}
