using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.Models.DTO
{
    public class EventClientDTO
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;
    }
}
