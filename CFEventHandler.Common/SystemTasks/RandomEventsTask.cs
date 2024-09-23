using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Tracing;

namespace CFEventHandler.SystemTasks
{
    /// <summary>
    /// System task to generate random events for testing
    /// </summary>
    public class RandomEventsTask : ISystemTask
    {
        public string Name => "Random test events";

        public bool IsRunOnStartup => false;

        private readonly SystemTaskSchedule _schedule;

        public RandomEventsTask(SystemTaskSchedule schedule)
        {
            _schedule = schedule;
        }

        public SystemTaskSchedule Schedule => _schedule;

        public async Task ExecuteAsync(CancellationToken cancellationToken, IServiceProvider serviceProvider, Dictionary<string, object> parameters)
        {
            try
            {
                _schedule.IsExecuting = true;

                var eventClientService = serviceProvider.GetRequiredService<IEventClientService>();
                var eventService = serviceProvider.GetRequiredService<IEventService>();
                var eventQueueService = serviceProvider.GetRequiredService<IEventQueueService>();
                var eventTypeService = serviceProvider.GetRequiredService<IEventTypeService>();

                // Get event client, event type
                var eventClient = await eventClientService.GetByNameAsync("Client 1");
                var eventType = await eventTypeService.GetByNameAsync("Test event 1");

                for (int index = 0; index < 10; index++)
                {
                    var eventInstance1 = new EventInstance()
                    {
                        //Id = Guid.NewGuid().ToString(),
                        CreatedDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(index)),
                        EventTypeId = eventType.Id,
                        EventClientId = eventClient.Id,
                        Parameters = new List<EventParameter>()
                    {
                        new EventParameter()
                        {
                            Name = "CompanyId",
                            Value = 1
                        },
                        new EventParameter()
                        {
                            Name = "Value3",
                            Value = true
                        },
                        new EventParameter()
                        {
                            Name = "Value2",
                            Value = "Test value"
                        }
                    }
                    };

                    // Save event
                    await eventService.AddAsync(eventInstance1);

                    // Add event to queue for processing
                    eventQueueService.Add(eventInstance1);
                }
            }
            finally
            {
                _schedule.IsExecuting = false;
            }
        }
    }
}
