using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Models;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.API.Services
{
    public class TenantDatabaseConfigService : ITenantDatabaseConfigService
    {
        private readonly IServiceProvider _serviceProvider;

        public TenantDatabaseConfigService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITenantDatabaseConfig GetCurrent()
        {
            var currentTenantContext = _serviceProvider.GetRequiredService<ICurrentTenantContext>();            
            var apiKeyInstance = _serviceProvider.GetRequiredService<IRequestInfoService>().APIKeyInstance;            

            Tenant tenant = null;
            if (currentTenantContext.TenantDatabaseConfig != null &&
                    !String.IsNullOrEmpty(currentTenantContext.TenantDatabaseConfig.ConnectionString))  // Outside of HTTP pipeline
            {
                return currentTenantContext.TenantDatabaseConfig;
            }
            else if (apiKeyInstance == null)   // No API key or invalid API key
            {
                tenant = new Tenant();                
            }
            else    // API key passed
            {
                var tenantCacheService = _serviceProvider.GetService<ITenantCacheService>();
                tenant = tenantCacheService.GetById(apiKeyInstance.TenantId);
            }
            return new TenantDatabaseConfig()
            {
                TenantId = tenant.Id,
                ConnectionString = tenant.ConnectionString,
                DatabaseName = tenant.DatabaseName
            };

        }
    }
}
