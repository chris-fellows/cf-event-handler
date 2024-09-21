using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Security;
using CFEventHandler.API.Validators;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using CFEventHandler.Process;
using CFEventHandler.Seed;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "APIKey", Roles = RoleNames.Admin)]
    //[SwaggerTag("Controller for admin functions")]
    public class AdminController : ControllerBase
    {
        private readonly IAPIKeyCacheService _apiKeyCacheService;
        private readonly IAPIKeyService _apiKeyService;
        private readonly IDatabaseAdminService _databaseAdminService;
        private readonly IEventClientService _eventClientService;
        private readonly IEventHandlerRuleService _eventHandlerRuleService;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IEventTypeService _eventTypeService;

        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISecurityAdminService _securityAdminService;
        private readonly ISignalRSettingsService _signalRSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ISQLSettingsService _sqlSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;
        private readonly ITenantService _tenantService;

        public AdminController(IAPIKeyCacheService apiKeyCacheService,
                        IAPIKeyService apiKeyService,
                        IDatabaseAdminService databaseAdminService,
                        IEventClientService eventClientService, 
                        IEventHandlerRuleService eventHandlerRuleService,
                        IEventHandlerService eventHandlerService,
                        IEventTypeService eventTypeService, 
                        IConsoleSettingsService consoleSettingsService, 
                        ICSVSettingsService csvSettingsService, 
                        IEmailSettingsService emailSettingsService, 
                        IHTTPSettingsService httpSettingsService, 
                        IProcessSettingsService processSettingsService, 
                        ISecurityAdminService securityAdminService,
                        ISignalRSettingsService signalRSettingsService,
                        ISMSSettingsService smsSettingsService, 
                        ISQLSettingsService sqlSettingsService,
                        ITeamsSettingsService teamsSettingsService,
                        ITenantService tenantService)
        {
            _apiKeyCacheService = apiKeyCacheService;
            _apiKeyService = apiKeyService;
            _databaseAdminService = databaseAdminService;
            _eventClientService = eventClientService;
            _eventHandlerRuleService = eventHandlerRuleService;
            _eventHandlerService = eventHandlerService;
            _eventTypeService = eventTypeService;
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _emailSettingsService = emailSettingsService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _securityAdminService = securityAdminService;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Deletes all data and creates shared data and tenant data using data for specific group
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateData")]
        public async Task<IActionResult> CreateData([FromQuery] int group = 1)
        {
            if (group < 0 || group < 1)
            {
                return Problem(title: "Group must be between 1 and 1", statusCode: (int)HttpStatusCode.BadRequest);
            }

            // Delete all data
            await _databaseAdminService.DeleteAllData();

            // Load shared data
            await _databaseAdminService.LoadSharedData(group);

            // Load tenant data for tenant #1
            var tenant = await _tenantService.GetByNameAsync("Tenant 1");
            await _databaseAdminService.LoadTenantData(tenant.Id, group);

            // Refresh API key cache
            _securityAdminService.RefreshAPIKeyCache();
        
            return Ok();
        }

        /// <summary>
        /// Creates all data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TestRead")]
        public async Task<IActionResult> ReadData()
        {
            var item1 = await _eventTypeService.GetByIdAsync("1");

            var item2 = _eventTypeService.GetAll().ToList();

            var eventTypeSeed = new EventTypeSeed1();
            await _eventTypeService.ExportAsync(eventTypeSeed);

            return Ok();
        }
    }
}
