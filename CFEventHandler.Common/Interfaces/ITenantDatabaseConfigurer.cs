namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Configures tenant database for use
    /// </summary>
    public interface ITenantDatabaseConfigurer
    {
        /// <summary>
        /// Initialise tenant database
        /// </summary>
        /// <returns></returns>
        Task InitialiseAsync(string tenantId);
    }
}
