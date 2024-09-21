using AutoMapper;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using CFEventHandler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Security;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using CFEventHandler.API.Validators;

namespace CFEventHandler.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    public class APIKeyController : ControllerBase
    {
        private readonly IAPIKeyService _apiKeyService;
        private readonly IMapper _mapper;
        private readonly IRequestInfoService _requestInfoService;
        private readonly ISecurityAdminService _securityAdmin;

        public APIKeyController(IAPIKeyService apiKeyService,
                              IMapper mapper,
                              IRequestInfoService requestInfoService,
                              ISecurityAdminService securityAdmin)
        {
            _apiKeyService = apiKeyService;
            _mapper = mapper;
            _requestInfoService = requestInfoService;
            _securityAdmin = securityAdmin;
        }

        /// <summary>
        /// Gets API key by Id
        /// </summary>
        /// <param name="id">API Key Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(APIKeyInstanceDTO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(string id)
        {
            // Get API key
            var apiKeyInstance = await _apiKeyService.GetByIdAsync(id);
            if (apiKeyInstance == null)
            {
                return NotFound();
            }

            // Map model to DTO
            var apiKeyInstanceDTO = _mapper.Map<APIKeyInstanceDTO>(apiKeyInstance);

            return Ok(apiKeyInstanceDTO);
        }

        /// <summary>
        /// Create API key
        /// </summary>
        /// <param name="eventClientDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(APIKeyInstanceDTO apiKeyInstanceDTO)
        {
            // Map 
            var apiKeyInstance = _mapper.Map<APIKeyInstance>(apiKeyInstanceDTO);
            apiKeyInstance.Id = String.Empty;
            apiKeyInstance.TenantId = _requestInfoService.TenantId; // Prevent updating another tenant

            // Prevent duplicate key
            var apiKeyInstances = _apiKeyService.GetAll().ToList();
            if (apiKeyInstances.Any(a => a.Key == apiKeyInstanceDTO.Key))
            {
                return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Key"), statusCode: (int)HttpStatusCode.BadRequest);
            }

            // Prevent duplicate name
            if (apiKeyInstances.Any(a => a.Name == apiKeyInstanceDTO.Name))
            {
                return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
            }

            // Save
            await _apiKeyService.AddAsync(apiKeyInstance);

            // Map model to DTO
            var newAPIKeyInstanceDTO = _mapper.Map<APIKeyInstanceDTO>(apiKeyInstance);

            return CreatedAtAction(nameof(GetById), new { id = newAPIKeyInstanceDTO.Id }, newAPIKeyInstanceDTO);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        /// <param name="eventClientDTO"></param>
        /// <param name="id">API Key Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(APIKeyInstanceDTO eventClientDTO, string id)
        {
            // Map 
            var apiKeyInstance = _mapper.Map<APIKeyInstance>(eventClientDTO);
            apiKeyInstance.Id = id;
            apiKeyInstance.TenantId = _requestInfoService.TenantId; // Prevent updating another tenant

            // Get from DB
            var apiKeyInstanceDB = await _apiKeyService.GetByIdAsync(id);
            if (apiKeyInstanceDB == null)
            {
                return NotFound();
            }

            // Prevent key change to same as other API key
            if (apiKeyInstance.Key != apiKeyInstanceDB.Key)  // Key changed
            {
                var apiKeyInstances = _apiKeyService.GetAll().ToList();
                var apiKeyInstanceSame = apiKeyInstances.FirstOrDefault(a => a.Key == apiKeyInstance.Key &&
                                                    a.Id != apiKeyInstanceDB.Id);
                if (apiKeyInstanceSame != null)
                {
                    return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Key"), statusCode: (int)HttpStatusCode.BadRequest);
                }
            }

            // Prevent name change to same as other API key
            if (apiKeyInstance.Name != apiKeyInstanceDB.Name)  // Name changed
            {
                var apiKeyInstances = _apiKeyService.GetAll().ToList();
                var apiKeyInstanceSame = apiKeyInstances.FirstOrDefault(a => a.Name == apiKeyInstance.Name &&
                                                    a.Id != apiKeyInstanceDB.Id);
                if (apiKeyInstanceSame != null)
                {
                    return Problem(title: ValidationMessageFormatter.PropertyMustByUnique("Name"), statusCode: (int)HttpStatusCode.BadRequest);
                }
            }
        
            // Save
            await _apiKeyService.AddAsync(apiKeyInstance);

            // Map model to DTO
            var newAPIKeyInstanceDTO = _mapper.Map<APIKeyInstanceDTO>(apiKeyInstance);

            return Ok(newAPIKeyInstanceDTO);
        }
    }
}
