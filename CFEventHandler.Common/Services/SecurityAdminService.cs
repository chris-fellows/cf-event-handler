using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.Services
{
    public class SecurityAdminService : ISecurityAdminService
    {
        private readonly IServiceProvider _serviceProvider;

        public SecurityAdminService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void RefreshAPIKeyCache()
        {           
            using (var scope = _serviceProvider.CreateScope())
            {
                // Clear API key cache
                var apiKeyCacheService = scope.ServiceProvider.GetRequiredService<IAPIKeyCacheService>();
                apiKeyCacheService.DeleteAll();

                // Load API keys for each tenant
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var tenants = tenantService.GetAll();
                foreach(var tenant in tenants)
                {
                    using (var tenantScope = scope.ServiceProvider.CreateScope())
                    {
                        // Set correct tenant for DI
                        ITenantDatabaseConfig tenantDatabaseConfig = new TenantDatabaseConfig()
                        {
                            TenantId = tenant.Id,
                            ConnectionString = tenant.ConnectionString,
                            DatabaseName = tenant.DatabaseName
                        };
                        var currentTenantContext = tenantScope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                        currentTenantContext.TenantDatabaseConfig = tenantDatabaseConfig;

                        var apiKeyService = tenantScope.ServiceProvider.GetRequiredService<IAPIKeyService>();
                        var apiKeys = apiKeyService.GetAll().ToList();

                        apiKeys.ForEach(apiKey => apiKeyCacheService.Add(apiKey, apiKey.Key));                        
                    }
                }
            }
        }
    }
}
