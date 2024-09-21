using CFEventHandler.Interfaces;

namespace CFEventHandler.API.Interfaces
{
    /// <summary>
    /// Interface for ITenantDatabaseConfig
    /// </summary>
    public interface ITenantDatabaseConfigService
    {
        /// <summary>
        /// Returns ITenantDatabaseConfig either from HTTP request or context set
        /// </summary>
        /// <returns></returns>
        ITenantDatabaseConfig GetCurrent();
    }
}
