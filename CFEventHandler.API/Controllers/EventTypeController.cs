using AutoMapper;
using CFEventHandler.API.Security;
using CFEventHandler.API.Validators;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Event type controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    //[SwaggerTag("Controller for event type data")]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;
        private readonly IMapper _mapper;

        public EventTypeController(IEventTypeService eventTypeService, IMapper mapper)
        {
            _eventTypeService = eventTypeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all event types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<EventTypeDTO>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            // Get event types
            var eventTypes = _eventTypeService.GetAll().OrderBy(et => et.Name).ToList();

            // Map models to DTO
            var eventTypeDTOs = _mapper.Map<List<EventTypeDTO>>(eventTypes);

            return Ok(eventTypeDTOs);
        }

        /// <summary>
        /// Gets all event types
        /// </summary>
        /// <param name="id">Event Type Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EventTypeDTO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(string id)
        {
            // Get event types
            var eventType = await _eventTypeService.GetByIdAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }

            // Map model to DTO
            var eventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

            return Ok(eventTypeDTO);
        }

        /// <summary>
        /// Create event type
        /// </summary>
        /// <param name="eventTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(EventTypeDTO eventTypeDTO)
        {
            // Map 
            var eventType = _mapper.Map<EventType>(eventTypeDTO);
            eventType.Id = String.Empty;

            // Prevent duplicate name
            var eventTypes = _eventTypeService.GetAll().ToList();
            if (eventTypes.Any(et => et.Name == eventType.Name))
            {
                return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
            }

            // Save
            await _eventTypeService.AddAsync(eventType);

            // Map model to DTO
            var newEventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

            return CreatedAtAction(nameof(GetById), new { id = newEventTypeDTO.Id }, newEventTypeDTO);            
        }

        /// <summary>
        /// Update event type
        /// </summary>
        /// <param name="eventTypeDTO"></param>
        /// <param name="id">Event Type Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EventTypeDTO eventTypeDTO, string id)
        {            
            // Map 
            var eventType = _mapper.Map<EventType>(eventTypeDTO);
            eventType.Id = id;

            // Get from DB
            var eventTypeDB = await _eventTypeService.GetByIdAsync(id);
            if (eventTypeDB == null)
            {
                return NotFound();
            }
            
            // Prevent name change to same name as other event type
            if (eventType.Name != eventTypeDB.Name)  // Name changed
            {
                var eventTypes = _eventTypeService.GetAll().ToList();
                var eventTypeSame = eventTypes.FirstOrDefault(et => et.Name == eventType.Name &&
                                                    et.Id != eventTypeDB.Id);
                if (eventTypeSame != null)
                {
                    return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
                }
            }

            // Save
            await _eventTypeService.AddAsync(eventType);

            // Map model to DTO
            var newEventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

            return Ok(newEventTypeDTO);
        }

        /// <summary>
        /// Delete event type
        /// </summary>
        /// <param name="id">Event Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]        
        public async Task<IActionResult> Delete(string id)
        {
            var eventType = await _eventTypeService.GetByIdAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }

            // TODO: Check dependencies
            await _eventTypeService.DeleteByIdAsync(eventType.Id);

            return Ok();
        }
    }
}
