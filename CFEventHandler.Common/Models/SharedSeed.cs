using CFEventHandler.Interfaces;

namespace CFEventHandler.Models
{
    public class SharedSeed
    {
        public IEntityList<Tenant> Tenants { get; set; }
    }
}
