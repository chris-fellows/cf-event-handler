using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using CFEventHandler.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;
using CFEventHandler.API.Security;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Logging controller
    /// </summary>
    [Route("[controller]")]
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
        [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.WriteEvent)]
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
        /// Returns filtered list of events logged
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.ReadEvent)]
        public async Task<IActionResult> GetFiltered([FromQuery] EventFilterDTO eventFilterDTO)
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
