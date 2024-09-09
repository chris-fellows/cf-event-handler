using CFEventHandler.API.Hubs;
using CFEventHandler.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace CFEventHandler.API.Services
{
    /// <summary>
    /// Background service for processing events
    /// 
    /// Currently there's just a single event consumer.
    /// </summary>
    public class EventBackgroundService : BackgroundService
    {
        private readonly IEventQueueService _eventQueueService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<NotificationHub> _hub;

        public EventBackgroundService(IEventQueueService eventQueueService,
                                      IHubContext<NotificationHub> hub,
                                      IServiceProvider serviceProvider)
        {
            _eventQueueService = eventQueueService;
            _hub = hub;
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEventManagerService eventManagerService = null;
            using (var scope = _serviceProvider.CreateScope())
            {
                eventManagerService = scope.ServiceProvider.GetService<IEventManagerService>();
            }

            //var lastNotification = DateTimeOffset.UtcNow;

            // Process event queue until stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get next event
                var eventInstance = _eventQueueService.GetNext();

                if (eventInstance != null)
                {
                    _hub.Clients.All.SendAsync("Status", $"Handling event {eventInstance.Id}").Wait();
                    eventManagerService.Handle(eventInstance);
                    _hub.Clients.All.SendAsync("Status", $"Handled event {eventInstance.Id}").Wait();
                    await Task.Delay(1, stoppingToken);
                }
                else
                {
                    await Task.Delay(5000, stoppingToken);
                }

                //if (lastNotification.AddSeconds(10) <= DateTimeOffset.UtcNow)
                //{
                //    lastNotification = DateTimeOffset.UtcNow;

                //    var eventInstance2 = new CFEventHandler.Models.EventInstance()
                //    {
                //         Id = Guid.NewGuid().ToString(),
                //         EventClientId = "1",
                //         EventTypeId = "2",
                //         Parameters = new List<Models.EventParameter>()
                //         {
                //             new Models.EventParameter() { Name = "P1", Value = 10 },
                //             new Models.EventParameter() { Name = "P2", Value = "Param2" },
                //         }
                //    };
                //    _hub.Clients.All.SendAsync("Event", eventInstance2).Wait();

                //    //_hub.Clients.All.SendAsync("TestMessage", "Test message").Wait();
                //    int xxx = 1000;
                //}
            }            
        }        
    }
}
