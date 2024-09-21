
using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.Models.DTO
{
    public class APIKeyInstanceDTO
    {
        public string Id { get; set; }

        public string TenantId { get; set; } = String.Empty;

        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;

        [MinLength(5)]
        [MaxLength(100)]
        public string Key { get; set; } = String.Empty;

        public List<string> Roles { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}
