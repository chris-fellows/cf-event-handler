﻿using CFEventHandler.API.Interfaces;
using CFEventHandler.API.Security;
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
        private readonly IDatabaseAdmin _databaseAdmin;
        private readonly IEventClientService _eventClientService;
        private readonly IEventHandlerRuleService _eventHandlerRuleService;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IEventTypeService _eventTypeService;

        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISecurityAdmin _securityAdmin;
        private readonly ISignalRSettingsService _signalRSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ISQLSettingsService _sqlSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;

        public AdminController(IAPIKeyCacheService apiKeyCacheService,
                        IAPIKeyService apiKeyService,
                        IDatabaseAdmin databaseAdmin,
                        IEventClientService eventClientService, 
                        IEventHandlerRuleService eventHandlerRuleService,
                        IEventHandlerService eventHandlerService,
                        IEventTypeService eventTypeService, 
                        IConsoleSettingsService consoleSettingsService, 
                        ICSVSettingsService csvSettingsService, 
                        IEmailSettingsService emailSettingsService, 
                        IHTTPSettingsService httpSettingsService, 
                        IProcessSettingsService processSettingsService, 
                        ISecurityAdmin securityAdmin,
                        ISignalRSettingsService signalRSettingsService,
                        ISMSSettingsService smsSettingsService, 
                        ISQLSettingsService sqlSettingsService,
                        ITeamsSettingsService teamsSettingsService)
        {
            _apiKeyCacheService = apiKeyCacheService;
            _apiKeyService = apiKeyService;
            _databaseAdmin = databaseAdmin;
            _eventClientService = eventClientService;
            _eventHandlerRuleService = eventHandlerRuleService;
            _eventHandlerService = eventHandlerService;
            _eventTypeService = eventTypeService;
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _emailSettingsService = emailSettingsService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _securityAdmin = securityAdmin;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
        }
  
        /// <summary>
        /// Creates all data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateData")]
        public async Task<IActionResult> CreateData()
        {
            try
            {
                await _databaseAdmin.LoadData(1);

                // Refresh API key cache
                _securityAdmin.RefreshAPIKeyCache();
            }
            catch(Exception exception)
            {
                throw;
            }

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
