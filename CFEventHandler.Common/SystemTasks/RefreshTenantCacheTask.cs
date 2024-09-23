using CFEventHandler.Interfaces;
using CFEventHandler.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System task to refresh tenant cache
    /// </summary>
    public class RefreshTenantCacheTask : ISystemTask
    {
        public string Name => "Refresh tenant cache";

        public bool IsRunOnStartup => true;

        private readonly SystemTaskSchedule _schedule;

        public RefreshTenantCacheTask(SystemTaskSchedule schedule)
        {
            _schedule = schedule;
        }

        public SystemTaskSchedule Schedule => _schedule;

        public async Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters)
        {
            try
            {
                _schedule.IsExecuting = true;

                var tenantAdminService = serviceProvider.GetRequiredService<ITenantAdminService>();                
                tenantAdminService.RefreshTenantCache();
            }
            finally
            {
                _schedule.IsExecuting = false;
            }
        }
    }
}
