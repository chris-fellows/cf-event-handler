using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System task to delete old events
    /// </summary>
    public class DeleteOldEventsTask : ISystemTask
    {
        public string Name => "Delete old events";
        
        private readonly SystemTaskSchedule _schedule;

        public DeleteOldEventsTask(SystemTaskSchedule schedule)
        {
            _schedule = schedule;
        }

        public SystemTaskSchedule Schedule => _schedule;

        public async Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters)
        {
            try
            {
                _schedule.Executing = true;

                // Get event service
                var eventService = serviceProvider.GetRequiredService<IEventService>();

                // Set event filter
                // TODO: Read from config
                var eventFilter = new EventFilter()
                {
                    FromCreatedDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365 * 10)),
                    ToCreatedDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(90))
                };

                // Delete events
                await eventService.DeleteByFilter(eventFilter);
            }
            finally
            {
                _schedule.Executing = false;
            }
        }
    }
}
