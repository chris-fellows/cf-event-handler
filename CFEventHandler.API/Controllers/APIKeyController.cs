using AutoMapper;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using CFEventHandler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Security;
using Microsoft.AspNetCore.Authorization;

namespace CFEventHandler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    public class APIKeyController : ControllerBase
    {
        private readonly IAPIKeyService _apiKeyService;
        private readonly IMapper _mapper;
        private readonly ISecurityAdmin _securityAdmin;

        public APIKeyController(IAPIKeyService apiKeyService,
                              IMapper mapper,
                              ISecurityAdmin securityAdmin)
        {
            _apiKeyService = apiKeyService;
            _mapper = mapper;
            _securityAdmin = securityAdmin;
        }

        ///// <summary>
        ///// Gets all event types
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    // Get event types
        //    var eventTypes = _eventTypeService.GetAll().OrderBy(et => et.Name).ToList();

        //    // Map models to DTO
        //    var eventTypeDTOs = _mapper.Map<List<EventTypeDTO>>(eventTypes);

        //    return Ok(eventTypeDTOs);
        //}

        ///// <summary>
        ///// Gets all event types
        ///// </summary>
        ///// <param name="id">Event Type Id</param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    // Get event types
        //    var eventType = await _eventTypeService.GetByIdAsync(id);
        //    if (eventType == null)
        //    {
        //        return NotFound();
        //    }

        //    // Map model to DTO
        //    var eventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

        //    return Ok(eventTypeDTO);
        //}

        ///// <summary>
        ///// Create event type
        ///// </summary>
        ///// <param name="eventTypeDTO"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Add(EventTypeDTO eventTypeDTO)
        //{
        //    // Map 
        //    var eventType = _mapper.Map<EventType>(eventTypeDTO);
        //    eventType.Id = String.Empty;

        //    // Save
        //    await _eventTypeService.AddAsync(eventType);

        //    // Map model to DTO
        //    var newEventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

        //    return CreatedAtAction(nameof(GetById), new { id = newEventTypeDTO.Id }, newEventTypeDTO);
        //}

        ///// <summary>
        ///// Update event type
        ///// </summary>
        ///// <param name="eventTypeDTO"></param>
        ///// <param name="id">Event Type Id</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(EventTypeDTO eventTypeDTO, string id)
        //{
        //    // Map 
        //    var eventType = _mapper.Map<EventType>(eventTypeDTO);
        //    eventType.Id = id;

        //    // Get from DB
        //    var eventTypeDB = await _eventTypeService.GetByIdAsync(id);
        //    if (eventTypeDB == null)
        //    {
        //        return NotFound();
        //    }

        //    // Save
        //    await _eventTypeService.AddAsync(eventType);

        //    // Map model to DTO
        //    var newEventTypeDTO = _mapper.Map<EventTypeDTO>(eventType);

        //    return Ok(newEventTypeDTO);
        //}

        ///// <summary>
        ///// Delete event type
        ///// </summary>
        ///// <param name="id">Event Type Id</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var eventType = await _eventTypeService.GetByIdAsync(id);
        //    if (eventType == null)
        //    {
        //        return NotFound();
        //    }

        //    // TODO: Check dependencies
        //    await _eventTypeService.DeleteByIdAsync(eventType.Id);

        //    return Ok();
        //}
    }
}
