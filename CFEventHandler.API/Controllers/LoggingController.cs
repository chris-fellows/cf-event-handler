using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using CFEventHandler.Interfaces;
using CFEventHandler.Common.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Logging controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[SwaggerTag("Controller for logging events")]
    public class LoggingController : ControllerBase
    {
        private readonly IEventQueueService _eventQueueService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;        

        public LoggingController(IEventQueueService eventQueueService,
                                IEventService eventService,
                                IMapper mapper)
        {
            _eventQueueService = eventQueueService;
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Logs event instance
        /// </summary>
        /// <param name="eventInstanceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Log([FromBody] EventInstanceDTO eventInstanceDTO)
        {
            // Map from DTO to model
            var eventInstance = _mapper.Map<EventInstance>(eventInstanceDTO);

            // Save for reporting
            await _eventService.AddAsync(eventInstance);

            // Add to queue
            _eventQueueService.Add(eventInstance);

            return Ok();
        }

        /// <summary>
        /// Returns list of events logged
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromBody] EventFilterDTO eventFilterDTO)
        {
            var eventFilter = _mapper.Map<EventFilter>(eventFilterDTO);

            // Get events
            var events = await _eventService.GetByFilter(eventFilter);

            // Map events to DTOs
            var eventDTOs = _mapper.Map<List<EventInstanceDTO>>(events);

            return Ok(eventDTOs);
        }
    }
}
