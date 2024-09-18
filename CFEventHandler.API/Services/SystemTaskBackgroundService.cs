using CFEventHandler.SystemTasks;
using System.Diagnostics;

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
             
        public  SystemTaskBackgroundService(IServiceProvider serviceProvider,
                                            ISystemTasks systemTasks)
        {
            _serviceProvider = serviceProvider;
            _systemTasks = systemTasks;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<TaskInfo> activeTaskInfos = new List<TaskInfo>();

            // Process system tasks until stopped
            while (!stoppingToken.IsCancellationRequested)
            {
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

                await Task.Delay(10000);
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
