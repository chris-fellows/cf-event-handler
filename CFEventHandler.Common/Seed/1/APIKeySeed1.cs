using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Common.Seed
{
    public class APIKeySeed1 : IEntityList<APIKeyInstance>
    {
        private readonly ITenantService _tenantService;

        public APIKeySeed1(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async Task<List<APIKeyInstance>> ReadAllAsync()
        {
            var tenants = _tenantService.GetAll();
            var tenant1 = tenants.First(t => t.Name == "Tenant 1");
            //var tenant2 = tenants.First(t => t.Name == "Tenant 2");

            var apiKeys = new List<APIKeyInstance>();

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 1 (Tenant 1)",
                Key = "111111",
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddDays(180),                
                Roles = new List<string>()
                {
                    "Admin"
                },
                TenantId = tenant1.Id
            });

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 2 (Tenant 1)",
                Key = "222222",
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "Admin"
                },
                TenantId = tenant1.Id
            });

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 3 (Tenant 1)",
                Key = "333333",
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "Admin"
                },
                TenantId = tenant1.Id
            });

            return apiKeys;
        }

        public async Task WriteAllAsync(List<APIKeyInstance> apiKeys)
        {
            // No action            
        }
    }
}
