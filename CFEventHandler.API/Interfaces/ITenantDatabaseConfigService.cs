using CFEventHandler.Interfaces;

namespace CFEventHandler.API.Interfaces
{
    public interface ITenantDatabaseConfigService
    {
        /// <summary>
        /// Returns ITenantDatabaseConfig either from HTTP request or context set
        /// </summary>
        /// <returns></returns>
        ITenantDatabaseConfig GetCurrent();
    }
}
