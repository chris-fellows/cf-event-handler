using CFEventHandler.Interfaces;

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

        public EventBackgroundService(IEventQueueService eventQueueService,
                                      IServiceProvider serviceProvider)
        {
            _eventQueueService = eventQueueService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEventManagerService eventManagerService = null;
            using (var scope = _serviceProvider.CreateScope())
            {
                eventManagerService = scope.ServiceProvider.GetService<IEventManagerService>();
            }

            // Process event queue until stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get next event
                var eventInstance = _eventQueueService.GetNext();

                if (eventInstance != null)
                {
                    eventManagerService.Handle(eventInstance);
                    await Task.Delay(1, stoppingToken);
                }
                else
                {
                    await Task.Delay(50000, stoppingToken);
                }
            }            
        }        
    }
}
