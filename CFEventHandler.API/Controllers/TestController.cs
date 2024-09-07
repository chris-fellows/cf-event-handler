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
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    { 
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
                Parameters  = new List<EventParameter>()
                {
                    new EventParameter()
                    {
                        Name = "Value1",
                        Value = 1
                    }
                }
            };

            var eventInstance2 = new EventInstance()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDateTime = DateTime.Now,
                EventTypeId = "2",
                Parameters = new List<EventParameter>()
                {
                    new EventParameter()
                    {
                        Name = "Value1",
                        Value = 2
                    }
                }
            };

            var eventInstance3 = new EventInstance()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDateTime = DateTime.Now,
                EventTypeId = "3",
                Parameters = new List<EventParameter>()
                {
                    new EventParameter()
                    {
                        Name = "Value1",
                        Value = 3
                    }
                }
            };

            return Ok();
        }
    }
}
