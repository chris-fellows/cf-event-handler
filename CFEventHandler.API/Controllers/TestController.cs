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
        private readonly IEventQueueService _eventQueueService;

        public TestController(IEventQueueService eventQueueService)
        {
            _eventQueueService = eventQueueService;
        }

        /// <summary>
        /// Adds test log entries
        /// </summary>
        /// <param name="eventInstanceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLogs")]
        public async Task<IActionResult> Log()
        {
            var eventInstance1 = new EventInstance()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDateTime = DateTime.Now,
                EventTypeId = "1",
                EventClientId = "1",                
                Parameters  = new List<EventParameter>()
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

            //var eventInstance2 = new EventInstance()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    CreatedDateTime = DateTime.Now,
            //    EventTypeId = "2",
            //    Parameters = new List<EventParameter>()
            //    {
            //        new EventParameter()
            //        {
            //            Name = "Value1",
            //            Value = 2
            //        }
            //    }
            //};

            //var eventInstance3 = new EventInstance()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    CreatedDateTime = DateTime.Now,
            //    EventTypeId = "3",
            //    Parameters = new List<EventParameter>()
            //    {
            //        new EventParameter()
            //        {
            //            Name = "Value1",
            //            Value = 3
            //        }
            //    }
            //};

            _eventQueueService.Add(eventInstance1);

            return Ok();
        }
    }
}
