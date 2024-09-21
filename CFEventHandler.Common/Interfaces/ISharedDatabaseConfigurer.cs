namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Configures shared database for use
    /// </summary>
    public interface ISharedDatabaseConfigurer
    {
        /// <summary>
        /// Initialise shared database
        /// </summary>
        /// <returns></returns>
        Task InitialiseAsync();
    }
}
