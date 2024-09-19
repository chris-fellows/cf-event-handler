using CFEventHandler.API.Security;
using CFEventHandler.Exceptions;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Test controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
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
        /// Tests API key
        /// </summary>        
        /// <returns></returns>        
        [HttpPost]        
        [Route("TestAPIKey")]
        public async Task<IActionResult> TestAPIKey()
        {
            int xxx = 1000;
            return Ok();
        }

        /// <summary>
        /// Test for no API key
        /// </summary>        
        /// <returns></returns>        
        [HttpGet]
        [Route("APIKey/None")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNoAPIKey()
        {
            var data = new Dictionary<string, object>();
            data.Add("Machine", Environment.MachineName);
            data.Add("User", Environment.UserName);

            return Ok(data);
        }

        /// <summary>
        /// Get request details
        /// </summary>        
        /// <returns></returns>        
        [HttpGet]
        [Route("Me")]
        public async Task<IActionResult> GetMe()
        {
            var data = new Dictionary<string, object>();
            data.Add("Machine", Environment.MachineName);
            data.Add("User", Environment.UserName);

            return Ok(data);
        }

        /// <summary>
        /// Tests API key for Admin role
        /// </summary>        
        /// <returns></returns>        
        [HttpGet]
        [Route("APIKey/Roles/Admin")]
        [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
        public async Task<IActionResult> GetTestAPIKeyAdmin()
        {                      
            return Ok();
        }

        /// <summary>
        /// Tests API key for ReadEvent role
        /// </summary>        
        /// <returns></returns>        
        [HttpGet]
        [Route("APIKey/Roles/ReadEvent")]
        [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.ReadEvent)]
        public async Task<IActionResult> GetTestAPIKeyReadEvent()
        {
            return Ok();
        }

        /// <summary>
        /// Tests API key for WriteEvent role
        /// </summary>        
        /// <returns></returns>        
        [HttpGet]
        [Route("APIKey/Roles/WriteEvent")]
        [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.WriteEvent)]
        public async Task<IActionResult> GetTestAPIKeyWriteEvent()
        {
            return Ok();
        }

        /// <summary>
        /// Tests unhandled error (Should get caught by ErrorMiddlewareService)
        /// </summary>        
        /// <returns></returns>        
        [HttpPost]
        [Route("TestUnhandledError")]
        public async Task<IActionResult> TestUnhandledError()
        {
            throw new GeneralException("Test unhandled error");            
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
