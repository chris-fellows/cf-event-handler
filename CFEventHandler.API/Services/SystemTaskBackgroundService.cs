﻿using CFEventHandler.SystemTasks;

namespace CFEventHandler.API.Services
{
    /// <summary>
    /// Background service for executing system tasks.
    /// 
    /// The following types of system task are executed:
    /// - During API startup. E.g. Tasks that need to execute before HTTP pipeline is processed.
    /// - Scheduled intervals. E.g. Every 60 mins.
    /// - Requested on demand. E.g. Refresh API key cache after new API key added.
    /// </summary>
    public class SystemTaskBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemTasks _systemTasks;

        private class TaskInfo
        {
            public Task Task { get; set; }

            public ISystemTask SystemTask { get; set; }
        }

        public SystemTaskBackgroundService(IServiceProvider serviceProvider,
                                            ISystemTasks systemTasks)
        {
            _serviceProvider = serviceProvider;
            _systemTasks = systemTasks;
        }

        /// <summary>
        /// Handles completed tasks
        /// 
        /// TODO: Implement logging
        /// </summary>
        /// <param name="taskInfos"></param>
        /// <returns></returns>
        private async Task HandleCompletedTasks(List<TaskInfo> taskInfos)
        {
            var completedTasks = taskInfos.Where(ti => ti.Task.IsCompleted).ToList();

            while (completedTasks.Any())
            {
                var completedTask = completedTasks.First();
                completedTasks.Remove(completedTask);
            }
        }

        /// <summary>
        /// Runs tasks that must run at startup before HTTP pipeline
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteStartupTasks(CancellationToken cancellationToken)
        {
            // Get system tasks to run
            var systemTasks = _systemTasks.AllTasks.Where(st => st.IsRunOnStartup).ToList();

            // Start each task
            List<TaskInfo> activeTaskInfos = new List<TaskInfo>();
            foreach (var systemTask in systemTasks)
            {
                // Wait if too many active tasks
                while (activeTaskInfos.Count >= _systemTasks.MaxConcurrentTasks)
                {
                    await HandleCompletedTasks(activeTaskInfos);
                    await Task.Delay(500);
                }

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                var taskInfo = new TaskInfo()
                {
                    Task = ExecuteSystemTaskAsync(cancellationTokenSource.Token, new Dictionary<string, object>(), systemTask),
                    SystemTask = systemTask
                };
                activeTaskInfos.Add(taskInfo);

                await HandleCompletedTasks(activeTaskInfos);
            }

            // Wait for tasks to complete
            while (activeTaskInfos.Any())
            {                
                await HandleCompletedTasks(activeTaskInfos);
                await Task.Delay(500);
            }
        }

        /// <summary>
        /// Executes overdue scheduled tasks
        /// </summary>
        /// <param name="activeTaskInfos"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task ExecuteScheduledTasks(List<TaskInfo> activeTaskInfos, CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested &&
                activeTaskInfos.Count < _systemTasks.MaxConcurrentTasks)
            {
                // Get overdue tasks
                var overdueTasks = _systemTasks.OverdueTasks.Where(st => !activeTaskInfos.Any(ti => ti.SystemTask.Name == st.Name)).ToList();

                // Start each overdue task, abort if max tasks executing
                while (overdueTasks.Any() &&
                    activeTaskInfos.Count < _systemTasks.MaxConcurrentTasks)
                {
                    // Get next task
                    var systemTask = overdueTasks.First();
                    overdueTasks.Remove(systemTask);

                    // Start task
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    var taskInfo = new TaskInfo()
                    {
                        Task = ExecuteSystemTaskAsync(cancellationTokenSource.Token, new Dictionary<string, object>(), systemTask),
                        SystemTask = systemTask
                    };
                    activeTaskInfos.Add(taskInfo);                    
                }
            }
        }

        /// <summary>
        /// Executes overdue requested tasks
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteRequestedTasks(List<TaskInfo> activeTaskInfos, CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested &&
                activeTaskInfos.Count < _systemTasks.MaxConcurrentTasks)
            {
                // Get system task to execute, not already executing
                var overdueTaskRequests = _systemTasks.OverdueRequests.Where(st => !activeTaskInfos.Any(ti => ti.SystemTask.Name == st.Name)).ToList();

                // Execute system task
                while (overdueTaskRequests.Any() &&
                    activeTaskInfos.Count < _systemTasks.MaxConcurrentTasks)
                {
                    // Get next task request
                    var systemTaskRequest = overdueTaskRequests.First();
                    overdueTaskRequests.Remove(systemTaskRequest);                    
                    var systemTask = _systemTasks.AllTasks.First(st => st.Name == systemTaskRequest.Name);

                    // Start task
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    var taskInfo = new TaskInfo()
                    {
                        Task = ExecuteSystemTaskAsync(cancellationTokenSource.Token, new Dictionary<string, object>(), systemTask),
                        SystemTask = systemTask
                    };
                    activeTaskInfos.Add(taskInfo);                    
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<TaskInfo> activeTaskInfos = new List<TaskInfo>();

            // Process system tasks until stopped. Ensure that active tasks are completed before leaving
            while (!stoppingToken.IsCancellationRequested ||
                    activeTaskInfos.Any())
            {
                // Check completed tasks
                await HandleCompletedTasks(activeTaskInfos);
              
                // Execute scheduled tasks                
                await ExecuteScheduledTasks(activeTaskInfos, stoppingToken);                

                // Execute requested tasks                
                await ExecuteRequestedTasks(activeTaskInfos, stoppingToken);                

                // Wait before next loop
                await Task.Delay(activeTaskInfos.Any() ||
                            _systemTasks.OverdueTasks.Any() ||
                            _systemTasks.OverdueRequests.Any() ? 500 : 10000);
            }            
        }

        /// <summary>
        /// Executes system task asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="parameters"></param>
        /// <param name="systemTask"></param>
        /// <returns></returns>
        protected Task ExecuteSystemTaskAsync(CancellationToken cancellationToken, Dictionary<string, object> parameters, ISystemTask systemTask)
        {
            return Task.Factory.StartNew(() =>
            {
                // Set last execute time to scheduled time
                systemTask.Schedule.LastExecuteTime = systemTask.Schedule.NextExecuteTime;

                // Execute task
                systemTask.ExecuteAsync(cancellationToken, _serviceProvider, parameters).Wait();
            });
        }
    }
}