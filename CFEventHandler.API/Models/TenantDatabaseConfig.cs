using CFEventHandler.Interfaces;

namespace CFEventHandler.API.Models
{
    public class TenantDatabaseConfig : DatabaseConfig, ITenantDatabaseConfig
    {
        public string TenantId { get; set; } = String.Empty;
    }
}
