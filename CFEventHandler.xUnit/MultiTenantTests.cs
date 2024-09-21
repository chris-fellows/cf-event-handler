using CFEventHandler.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.xUnit
{
    /// <summary>
    /// Tests for multi-tenant
    /// </summary>
    public class MultiTenantTests : FactoryTestsBase, IClassFixture<WebApplicationFactory<Program>>
    {
        public MultiTenantTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            
        }

        [Fact]
        public async Task Tenant_Resolved_If_No_HTTP_Request_And_Tenant_Set()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Get tenant to use
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var tenants = tenantService.GetAll();
                var tenant = tenants.Last();    // Use last incase code defaults to first

                // Set current tenant context
                var currentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                //currentTenantContext.TenantDatabaseConfig = new 

                // Resolve ITenantDatabaseConfig
                var tenantDatabaseConfig = scope.ServiceProvider.GetRequiredService<ITenantDatabaseConfig>();
                Assert.Equal(tenant.DatabaseName, tenantDatabaseConfig.DatabaseName);                
            }
        }
        [Fact]
        public async Task Tenant_Not_Resolved_If_No_HTTP_Request_And_Tenant_Not_Set()
        {
            using (var scope = _factory.Services.CreateScope())
            {                
                // Clear current tenant context
                var currentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                currentTenantContext.TenantDatabaseConfig = null;

                // Resolve ITenantDatabaseConfig
                var tenantDatabaseConfig = scope.ServiceProvider.GetRequiredService<ITenantDatabaseConfig>();
                Assert.True(String.IsNullOrEmpty(tenantDatabaseConfig.ConnectionString));                
            }
        }
    }
}
