using CFEventHandler.Interfaces;

namespace CFEventHandler.Models
{
    public class SharedSeed
    {
        public IEntityReader<Tenant> Tenants { get; set; }
    }
}
