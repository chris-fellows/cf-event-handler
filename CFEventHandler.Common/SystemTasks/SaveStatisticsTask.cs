using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.SystemTasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System task to save system statistics
    /// </summary>
    public class SaveStatisticsTask : ISystemTask
    {
        public string Name => "Save statistics";

        public bool IsRunOnStartup => false;

        private readonly SystemTaskSchedule _schedule;

        public SaveStatisticsTask(SystemTaskSchedule schedule)
        {
            _schedule = schedule;
        }

        public SystemTaskSchedule Schedule => _schedule;

        public async Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters)
        {
            try
            {
                _schedule.Executing = true;
                
                // TODO: Implement this                            
            }
            finally
            {
                _schedule.Executing = false;
            }
        }
    }
}
