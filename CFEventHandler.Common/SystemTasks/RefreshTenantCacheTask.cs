using CFEventHandler.Interfaces;
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
                _schedule.Executing = true;

                var tenantAdmin = serviceProvider.GetRequiredService<ITenantAdmin>();
                tenantAdmin.RefreshTenantCache();
            }
            finally
            {
                _schedule.Executing = false;
            }
        }
    }
}
