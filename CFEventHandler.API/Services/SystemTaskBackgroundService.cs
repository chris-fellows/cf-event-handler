using CFEventHandler.SystemTasks;

namespace CFEventHandler.API.Services
{
    /// <summary>
    /// Background service for executing system tasks
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
            //var completedTasks = taskInfos.Where(ti => ti.Task.IsCompleted).ToList();

            //while (completedTasks.Any())
            //{
            //    var completedTask = completedTasks.First();
            //    completedTasks.Remove(completedTask);
            //}
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
                // Check completed tasks
                /await HandleCompletedTasks(activeTaskInfos);
                await Task.Delay(500);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<TaskInfo> activeTaskInfos = new List<TaskInfo>();

            // Process system tasks until stopped
            while (!stoppingToken.IsCancellationRequested ||
                    activeTaskInfos.Any())
            {
                // Wait if too many active tasks
                while (activeTaskInfos.Count >= _systemTasks.MaxConcurrentTasks)
                {
                    await HandleCompletedTasks(activeTaskInfos);
                    await Task.Delay(500);
                }

                // Get system task to execute, not already executing
                var systemTaskToExecute = _systemTasks.OverdueTasks.FirstOrDefault(st => !activeTaskInfos.Any(ti => ti.SystemTask.Name == st.Name));

                // Execute system task
                if (systemTaskToExecute != null)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    var taskInfo = new TaskInfo()
                    {
                        Task = ExecuteSystemTaskAsync(cancellationTokenSource.Token, new Dictionary<string, object>(), systemTaskToExecute),
                        SystemTask = systemTaskToExecute
                    };
                    activeTaskInfos.Add(taskInfo);
                }
            }

            // Check completed tasks
            //await HandleCompletedTasks(activeTaskInfos);

            await Task.Delay(10000);
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