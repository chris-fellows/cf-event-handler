using CFEventHandler.API.Interfaces;
using CFEventHandler.Interfaces;

namespace CFEventHandler.API.Services
{
    public class CurrentTenantContext : ICurrentTenantContext
    {
        public ITenantDatabaseConfig TenantDatabaseConfig { get; set; }
    }
}
