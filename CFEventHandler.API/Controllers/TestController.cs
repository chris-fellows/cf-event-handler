using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Test controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {        
        private readonly IEventClientService _eventClientService;
        private readonly IEventQueueService _eventQueueService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;

        public TestController(IEventClientService eventClientService,
                            IEventQueueService eventQueueService,
                            IEventService eventService,
                            IEventTypeService eventTypeService)
        {
            _eventClientService = eventClientService;
            _eventQueueService = eventQueueService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
        }     

        /// <summary>
        /// Adds test log entries (1 or more)
        /// </summary>
        /// <param name="eventInstanceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLogs")]
        public async Task<IActionResult> Logs([FromQuery] int eventCount = 1)
        {          
            // Get event client, event type
            var eventClient = await _eventClientService.GetByNameAsync("Client 1");
            var eventType = await _eventTypeService.GetByNameAsync("Test event 1");

            for (int index = 0; index < eventCount; index++)
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
                await _eventService.AddAsync(eventInstance1);

                // Add event to queue for processing
                _eventQueueService.Add(eventInstance1);
            }

            return Ok();
        }
    }
}
