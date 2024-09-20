using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Seed
{
    public class TenantSeed1 : IEntityList<Tenant>
    {
        public async Task<List<Tenant>> ReadAllAsync()
        {
            var tenants = new List<Tenant>();

            tenants.Add(new Tenant()
            {
                Name = "Tenant 1",
                DatabaseName = "event_handler_1",
                ConnectionString = "NOT_SET"     // Shouldn't put connection strings in code
            });

            tenants.Add(new Tenant()
            {
                Name = "Tenant 2",
                DatabaseName = "event_handler_2",
                ConnectionString = "NOT_SET"    // Shouldn't put connection strings in code
            });

            return tenants;
        }

        public async Task WriteAllAsync(List<Tenant> tenantList)
        {
            // No action            
        }
    }
}
