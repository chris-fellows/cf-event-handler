using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for tenant cache
    /// </summary>
    public interface ITenantCacheService : ICacheService<Tenant, string>
    {

    }
}
