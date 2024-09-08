using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using CFEventHandler.Process;
using CFEventHandler.Seed;
using CFEventHandler.SMS;
using CFEventHandler.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFEventHandler.API.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[SwaggerTag("Controller for admin functions")]
    public class AdminController : ControllerBase
    {
        private readonly IEventClientService _eventClientService;
        private readonly IEventHandlerRuleService _eventHandlerRuleService;
        private readonly IEventTypeService _eventTypeService;

        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;

        public AdminController(IEventClientService eventClientService, 
                        IEventHandlerRuleService eventHandlerRuleService,
                        IEventTypeService eventTypeService, 
                        IConsoleSettingsService consoleSettingsService, 
                        ICSVSettingsService csvSettingsService, 
                        IEmailSettingsService emailSettingsService, 
                        IHTTPSettingsService httpSettingsService, 
                        IProcessSettingsService processSettingsService, 
                        ISMSSettingsService smsSettingsService, 
                        ITeamsSettingsService teamsSettingsService)
        {
            _eventClientService = eventClientService;
            _eventHandlerRuleService = eventHandlerRuleService;
            _eventTypeService = eventTypeService;
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _emailSettingsService = emailSettingsService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _smsSettingsService = smsSettingsService;
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
            // Base data
            await _eventClientService.ImportAsync(new EventClientSeed1());
            await _eventHandlerRuleService.ImportAsync(new EventHandlerRuleSeed1());
            await _eventTypeService.ImportAsync(new EventTypeSeed1());

            // Event settings
            await _consoleSettingsService.ImportAsync(new ConsoleEventSettingsSeed1());
            await _csvSettingsService.ImportAsync(new CSVEventSettingsSeed1());
            await _emailSettingsService.ImportAsync(new EmailEventSettingsSeed1());
            await _httpSettingsService.ImportAsync(new HTTPEventSettingsSeed1());
            await _processSettingsService.ImportAsync(new ProcessEventSettingsSeed1());
            await _smsSettingsService.ImportAsync(new SMSEventSettingsSeed1());
            await _teamsSettingsService.ImportAsync(new TeamsEventSettingsSeed1());

            return Ok();
        }
    }
}
