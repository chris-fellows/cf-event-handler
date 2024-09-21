using CFEventHandler.Interfaces;

namespace CFEventHandler.Models
{
    public class TenantDatabaseConfig : DatabaseConfig, ITenantDatabaseConfig
    {
        public string TenantId { get; set; } = String.Empty;
    }
}
