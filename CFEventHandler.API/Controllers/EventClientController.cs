using AutoMapper;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using CFEventHandler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CFEventHandler.API.Security;
using System.Net;
using CFEventHandler.API.Validators;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Event client controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    //[SwaggerTag("Controller for event client data")]
    public class EventClientController : ControllerBase
    {
        private readonly IEventClientService _eventClientService;

        private readonly IMapper _mapper;

        public EventClientController(IEventClientService eventClientService, IMapper mapper)
        {
            _eventClientService = eventClientService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all event clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<EventClientDTO>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            // Get event clients
            var eventClients = _eventClientService.GetAll().OrderBy(ec => ec.Name).ToList();

            // Map models to DTO
            var eventClientDTOs = _mapper.Map<List<EventClientDTO>>(eventClients);

            return Ok(eventClientDTOs);
        }

        /// <summary>
        /// Gets all event clients
        /// </summary>
        /// <param name="id">Event Client Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EventClientDTO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(string id)
        {
            // Get event client
            var eventClient = await _eventClientService.GetByIdAsync(id);
            if (eventClient == null)
            {
                return NotFound();
            }

            // Map model to DTO
            var eventClientDTO = _mapper.Map<EventClientDTO>(eventClient);

            return Ok(eventClientDTO);
        }

        /// <summary>
        /// Create event client
        /// </summary>
        /// <param name="eventClientDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(EventClientDTO eventClientDTO)
        {
            // Map 
            var eventClient = _mapper.Map<EventClient>(eventClientDTO);
            eventClient.Id = String.Empty;

            // Prevent duplicate name
            var eventClients = _eventClientService.GetAll().ToList();
            if (eventClients.Any(ec => ec.Name == eventClient.Name))
            {
                return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
            }

            // Save
            await _eventClientService.AddAsync(eventClient);

            // Map model to DTO
            var newEventClientDTO = _mapper.Map<EventTypeDTO>(eventClient);

            return CreatedAtAction(nameof(GetById), new { id = newEventClientDTO.Id }, newEventClientDTO);
        }

        /// <summary>
        /// Update event client
        /// </summary>
        /// <param name="eventClientDTO"></param>
        /// <param name="id">Event Client Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EventClientDTO eventClientDTO, string id)
        {
            // Map 
            var eventClient = _mapper.Map<EventClient>(eventClientDTO);
            eventClient.Id = id;

            // Get from DB
            var eventClientDB = await _eventClientService.GetByIdAsync(id);
            if (eventClientDB == null)
            {
                return NotFound();
            }

            // Prevent name change to same name as other event client
            if (eventClient.Name != eventClientDB.Name)  // Name changed
            {
                var eventClients = _eventClientService.GetAll().ToList();
                var eventClientSame = eventClients.FirstOrDefault(ec => ec.Name == eventClient.Name &&
                                                    ec.Id != eventClientDB.Id);
                if (eventClientSame != null)
                {
                    return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
                }
            }

            // Save
            await _eventClientService.AddAsync(eventClient);

            // Map model to DTO
            var newEventClientDTO = _mapper.Map<EventClientDTO>(eventClient);

            return Ok(newEventClientDTO);
        }

        /// <summary>
        /// Delete event client
        /// </summary>
        /// <param name="id">Event Client Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var eventClient = await _eventClientService.GetByIdAsync(id);
            if (eventClient == null)
            {
                return NotFound();
            }

            // TODO: Check dependencies
            await _eventClientService.DeleteByIdAsync(eventClient.Id);

            return Ok();
        }
    }
}
