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
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;
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
                        IServiceProvider serviceProvider,
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
            _serviceProvider = serviceProvider;
            _signalRSettingsService = signalRSettingsService;
            _smsSettingsService = smsSettingsService;
            _sqlSettingsService = sqlSettingsService;
            _teamsSettingsService = teamsSettingsService;
            _tenantService = tenantService;
        }      

        public TenantSeed GetSeedData(int group)
        {
            var tenantSeed = new TenantSeed();
            
                    tenantSeed.APIKeys = _serviceProvider.GetRequiredKeyedService<IEntityReader<APIKeyInstance>>($"APIKeySeed{group}");
                    tenantSeed.ConsoleEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<ConsoleEventSettings>>($"ConsoleEventSettingsSeed{group}");
                    tenantSeed.DocumentTemplates = _serviceProvider.GetRequiredKeyedService<IEntityReader<DocumentTemplate>>($"DocumentTemplateSeed{group}");
                    tenantSeed.EmailEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<EmailEventSettings>>($"EmailEventSettingsSeed{group}");
                    tenantSeed.EventClients = _serviceProvider.GetRequiredKeyedService<IEntityReader<EventClient>>($"EventClientSeed{group}");
                    tenantSeed.EventHandlerRules = _serviceProvider.GetRequiredKeyedService<IEntityReader<EventHandlerRule>>($"EventHandlerRuleSeed{group}");                    
                    tenantSeed.EventHandlers = _serviceProvider.GetRequiredKeyedService<IEntityReader<CFEventHandler.Models.EventHandler>>($"EventHandlerSeed{group}");
                    tenantSeed.EventTypes = _serviceProvider.GetRequiredKeyedService<IEntityReader<EventType>>($"EventTypeSeed{group}");
                    tenantSeed.HTTPEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<HTTPEventSettings>>($"HTTPEventSettingsSeed{group}");
                    tenantSeed.ProcessEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<ProcessEventSettings>>($"ProcessEventSettingsSeed{group}");
                    tenantSeed.SignalREventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<SignalREventSettings>>($"SignalREventSettingsSeed{group}");
                    tenantSeed.SMSEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<SMSEventSettings>>($"SMSEventSettingsSeed{group}");
                    tenantSeed.SQLEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<SQLEventSettings>>($"SQLEventSettingsSeed{group}");
                    tenantSeed.TeamsEventSettings = _serviceProvider.GetRequiredKeyedService<IEntityReader<TeamsEventSettings>>($"TeamsEventSettingsSeed{group}");

            /*
            tenantSeed.EventHandlerRules = new EventHandlerRuleSeed1(_consoleSettingsService, _csvSettingsService,
                                        _emailSettingsService, _eventHandlerService,
                                        _eventTypeService, _httpSettingsService,
                                        _processSettingsService, _signalRSettingsService,
                                        _smsSettingsService, _sqlSettingsService,
                                        _teamsSettingsService);
            */

            return tenantSeed;
        }
    }
}
