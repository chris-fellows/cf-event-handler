namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for database admin
    /// </summary>
    public interface IDatabaseAdmin
    {
        /// <summary>
        /// Initialises data
        /// </summary>
        /// <returns></returns>
        Task InitialiseAsync();

        /// <summary>
        /// Deletes all data
        /// </summary>
        /// <returns></returns>
        Task DeleteAllData();

        /// <summary>
        /// Loads data for specified group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task LoadData(int group);
    }
}
