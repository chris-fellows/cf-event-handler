namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System tasks list
    /// </summary>
    public interface ISystemTasks
    {
        /// <summary>
        /// All system tasks
        /// </summary>
        List<ISystemTask> AllTasks { get; }

        /// <summary>
        /// Active system tasks
        /// </summary>
        List<ISystemTask> ActiveTasks { get; }

        /// <summary>
        /// Overdue system tasks
        /// </summary>
        /// <returns></returns>
        List<ISystemTask> OverdueTasks { get; }
    }
}
