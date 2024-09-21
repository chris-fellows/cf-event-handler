using CFEventHandler.Common.Seed;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using CFEventHandler.Seed;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Services
{
    public class TenantSeedDataService : ITenantSeedDataService
    {
        private readonly IConsoleSettingsService _consoleSettingsService;
        private readonly ICSVSettingsService _csvSettingsService;
        private readonly IDocumentTemplateService _documentTemplateService;
        private readonly IEmailSettingsService _emailSettingsService;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IHTTPSettingsService _httpSettingsService;
        private readonly IProcessSettingsService _processSettingsService;
        private readonly ISignalRSettingsService _signalRSettingsService;
        private readonly ISMSSettingsService _smsSettingsService;
        private readonly ISQLSettingsService _sqlSettingsService;
        private readonly ITeamsSettingsService _teamsSettingsService;
        private readonly ITenantService _tenantService;

        public TenantSeedDataService(IConsoleSettingsService consoleSettingsService,
                        ICSVSettingsService csvSettingsService,
                        IDocumentTemplateService documentTemplateService,
                        IEmailSettingsService emailSettingsService,
                        IEventHandlerService eventHandlerService,
                        IEventTypeService eventTypeService,
                        IHTTPSettingsService httpSettingsService,
                        IProcessSettingsService processSettingsService,
                        ISignalRSettingsService signalRSettingsService,
                        ISMSSettingsService smsSettingsService,
                        ISQLSettingsService sqlSettingsService,
                        ITeamsSettingsService teamsSettingsService,
                        ITenantService tenantService)
        {
            _consoleSettingsService = consoleSettingsService;
            _csvSettingsService = csvSettingsService;
            _documentTemplateService = documentTemplateService;
            _emailSettingsService = emailSettingsService;
            _eventHandlerService = eventHandlerService;
            _eventTypeService = eventTypeService;
            _httpSettingsService = httpSettingsService;
            _processSettingsService = processSettingsService;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
            _tenantService = tenantService;
        }      

        public TenantSeed GetSeedData(int group)
        {
            var tenantSeed = new TenantSeed();

            switch (group)
            {
                case 1:
                    tenantSeed.APIKeys = new APIKeySeed1(_tenantService);
                    tenantSeed.ConsoleEventSettings = new ConsoleEventSettingsSeed1();
                    tenantSeed.CSVEventSettings = new CSVEventSettingsSeed1();
                    tenantSeed.DocumentTemplates = new DocumentTemplateSeed1();
                    tenantSeed.EmailEventSettings = new EmailEventSettingsSeed1(_documentTemplateService);
                    tenantSeed.EventClients = new EventClientSeed1();
                    tenantSeed.EventHandlerRules = new EventHandlerRuleSeed1(_consoleSettingsService, _csvSettingsService,
                                        _emailSettingsService, _eventHandlerService,
                                        _eventTypeService, _httpSettingsService,
                                        _processSettingsService, _signalRSettingsService,
                                        _smsSettingsService, _sqlSettingsService,
                                        _teamsSettingsService);
                    tenantSeed.EventHandlers = new EventHandlerSeed1();
                    tenantSeed.EventTypes = new EventTypeSeed1();
                    tenantSeed.HTTPEventSettings = new HTTPEventSettingsSeed1();
                    tenantSeed.ProcessEventSettings = new ProcessEventSettingsSeed1();
                    tenantSeed.SignalREventSettings = new SignalREventSettingsSeed1();
                    tenantSeed.SMSEventSettings = new SMSEventSettingsSeed1();
                    tenantSeed.SQLEventSettings = new SQLEventSettingsSeed1();
                    tenantSeed.TeamsEventSettings = new TeamsEventSettingsSeed1();
                    break;
            }

            return tenantSeed;
        }
    }
}
