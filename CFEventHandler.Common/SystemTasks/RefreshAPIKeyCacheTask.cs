﻿using CFEventHandler.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System task to refresh API key cache
    /// </summary>
    public class RefreshAPIKeyCacheTask : ISystemTask
    {
        public string Name => "Refresh API key cache";

        public bool IsRunOnStartup => true;

        private readonly SystemTaskSchedule _schedule;

        public RefreshAPIKeyCacheTask(SystemTaskSchedule schedule)
        {
            _schedule = schedule;
        }

        public SystemTaskSchedule Schedule => _schedule;

        public async Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters)
        {
            try
            {
                _schedule.IsExecuting = true;

                var securityAdminService = serviceProvider.GetRequiredService<ISecurityAdminService>();                
                securityAdminService.RefreshAPIKeyCache();
            }
            finally
            {
                _schedule.IsExecuting = false;
            }
        }
    }
}
