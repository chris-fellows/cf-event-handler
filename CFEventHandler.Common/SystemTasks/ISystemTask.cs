namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// Interface for system task
    /// </summary>
    public interface ISystemTask
    {
        /// <summary>
        /// Task name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Task schedule
        /// </summary>
        SystemTaskSchedule Schedule { get; }

        /// <summary>
        /// Execute task asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters);
    }
}
