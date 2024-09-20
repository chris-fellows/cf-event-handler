using CFEventHandler.Interfaces;

namespace CFEventHandler.Services
{
    public class TenantAdmin : ITenantAdmin
    {
        private readonly ITenantCacheService _tenantCacheService;
        private readonly ITenantService _tenantService;
        
        public TenantAdmin(ITenantCacheService tenantCacheService,
            ITenantService tenantService)
        {
            _tenantCacheService = tenantCacheService;
            _tenantService = tenantService;
        }


        public void RefreshTenantCache()
        {
            _tenantCacheService.DeleteAll();

            var tenants = _tenantService.GetAll().ToList();
            tenants.ForEach(tenant => _tenantCacheService.Add(tenant, tenant.Id));
        }
    }
}
