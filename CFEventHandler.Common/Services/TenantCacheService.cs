using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Services;

namespace CFEventHandler.Services
{
    public class TenantCacheService : CacheService<Tenant, string>, ITenantCacheService
    {

    }
}
