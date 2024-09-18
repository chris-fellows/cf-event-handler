using AutoMapper;
using CFEventHandler.API.Security;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Event handler controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    //[SwaggerTag("Controller for event handler data")]
    public class EventHandlerController : ControllerBase
    {
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IMapper _mapper;

        public EventHandlerController(IEventHandlerService eventHandlerService, IMapper mapper)
        {
            _eventHandlerService = eventHandlerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all event handlers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get event handles
            var eventHandlers = _eventHandlerService.GetAll().OrderBy(eh => eh.Name).ToList();

            // Map models to DTO
            var eventHandlerDTOs = _mapper.Map<List<EventHandlerDTO>>(eventHandlers);

            return Ok(eventHandlerDTOs);
        }
    }
}
