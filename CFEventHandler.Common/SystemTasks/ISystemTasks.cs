﻿namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System tasks list
    /// </summary>
    public interface ISystemTasks
    {
        /// <summary>
        /// Max number of concurrent tasks allowed
        /// </summary>
        int MaxConcurrentTasks { get; }

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

        /// <summary>
        /// All requests to execute system tasks
        /// </summary>
        List<SystemTaskRequest> AllRequests { get; }

        /// <summary>
        /// Overdue requests to execute system tasks
        /// </summary>
        List<SystemTaskRequest> OverdueRequests { get; }
    }
}
