namespace CFEventHandler.Interfaces
{
    public interface ICurrentTenantContext
    {
        ITenantDatabaseConfig TenantDatabaseConfig { get; set; }
    }
}
