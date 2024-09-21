using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Process;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.Services
{
    public class DatabaseAdminService : IDatabaseAdminService
    {
        private readonly IAPIKeyService _apiKeyService;
        private readonly IDatabaseConfig _databaseConfig;
        private readonly ISharedDatabaseConfigurer _sharedDatabaseConfigurer;
        private readonly ITenantDatabaseConfigurer _tenantDatabaseConfigurer;
        private readonly ITenantService _tenantService;
        
        private readonly IServiceProvider _serviceProvider;

        public DatabaseAdminService(IAPIKeyService apiKeyService,                        
                        IDatabaseConfig databaseConfig,                       
                        IServiceProvider serviceProvider,
                        ISharedDatabaseConfigurer sharedDatabaseConfigurer,
                        ITenantDatabaseConfigurer tenantDatabaseConfigurer,
                        ITenantService tenantService)
        {
            _apiKeyService = apiKeyService;
            _databaseConfig = databaseConfig;         
            _serviceProvider = serviceProvider;
            _sharedDatabaseConfigurer = sharedDatabaseConfigurer;
            _tenantDatabaseConfigurer= tenantDatabaseConfigurer;
            _tenantService = tenantService;            
        }        

        public async Task InitialiseSharedAsync()
        {
            await _sharedDatabaseConfigurer.InitialiseAsync();            
        }

        public async Task InitialiseTenantAsync(string tenantId)
        {
            await _tenantDatabaseConfigurer.InitialiseAsync(tenantId);
        }

        public async Task LoadTenantData(string tenantId, int group)
        {
            var tenantService = _serviceProvider.GetRequiredService<ITenantService>();
            var tenant = await tenantService.GetByIdAsync(tenantId);                     

            using (var tenantScope = _serviceProvider.CreateScope())
            {
                // Set correct tenant for DI
                ITenantDatabaseConfig tenantDatabaseConfig = new TenantDatabaseConfig()
                {
                    TenantId = tenant.Id,
                    ConnectionString = tenant.ConnectionString,
                    DatabaseName = tenant.DatabaseName
                };
                var currentTenantContext = tenantScope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                currentTenantContext.TenantDatabaseConfig = tenantDatabaseConfig;
                
                var apiKeyService = tenantScope.ServiceProvider.GetRequiredService<IAPIKeyService>();
                var documentTemplateService = tenantScope.ServiceProvider.GetRequiredService<IDocumentTemplateService>();
                var eventClientService = tenantScope.ServiceProvider.GetRequiredService<IEventClientService>();
                var eventHandlerService = tenantScope.ServiceProvider.GetRequiredService<IEventHandlerService>();
                var eventHandlerRuleService = tenantScope.ServiceProvider.GetRequiredService<IEventHandlerRuleService>();
                var eventTypeService = tenantScope.ServiceProvider.GetRequiredService<IEventTypeService>();

                var consoleSettingsService = tenantScope.ServiceProvider.GetRequiredService<IConsoleSettingsService>();
                var csvSettingsService = tenantScope.ServiceProvider.GetRequiredService<ICSVSettingsService>();
                var emailSettingsService = tenantScope.ServiceProvider.GetRequiredService<IEmailSettingsService>();
                var httpSettingsService = tenantScope.ServiceProvider.GetRequiredService<IHTTPSettingsService>();
                var processSettingsService = tenantScope.ServiceProvider.GetRequiredService<IProcessSettingsService>();
                var signalRSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISignalRSettingsService>();
                var smsSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISMSSettingsService>();
                var sqlSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISQLSettingsService>();
                var teamsSettingsService = tenantScope.ServiceProvider.GetRequiredService<ITeamsSettingsService>();

                ITenantSeedDataService seedDataService = new TenantSeedDataService(consoleSettingsService, csvSettingsService,
                     documentTemplateService, emailSettingsService,
                     eventHandlerService, eventTypeService,
                     httpSettingsService, processSettingsService,
                     signalRSettingsService, smsSettingsService,
                     sqlSettingsService, teamsSettingsService, tenantService);

                // Get tenant seed
                var tenantSeed = seedDataService.GetSeedData(group);

                // Base data                    
                await apiKeyService.ImportAsync(tenantSeed.APIKeys);
                await documentTemplateService.ImportAsync(tenantSeed.DocumentTemplates);
                await eventClientService.ImportAsync(tenantSeed.EventClients);
                await eventHandlerService.ImportAsync(tenantSeed.EventHandlers);
                await eventTypeService.ImportAsync(tenantSeed.EventTypes);

                // Event settings
                await consoleSettingsService.ImportAsync(tenantSeed.ConsoleEventSettings);
                await csvSettingsService.ImportAsync(tenantSeed.CSVEventSettings);
                await emailSettingsService.ImportAsync(tenantSeed.EmailEventSettings);
                await httpSettingsService.ImportAsync(tenantSeed.HTTPEventSettings);
                await processSettingsService.ImportAsync(tenantSeed.ProcessEventSettings);
                await signalRSettingsService.ImportAsync(tenantSeed.SignalREventSettings);
                await smsSettingsService.ImportAsync(tenantSeed.SMSEventSettings);
                await sqlSettingsService.ImportAsync(tenantSeed.SQLEventSettings);
                await teamsSettingsService.ImportAsync(tenantSeed.TeamsEventSettings);

                // Event handler rules. Needs to be done at the end because it depends on event settings etc
                await eventHandlerRuleService.ImportAsync(tenantSeed.EventHandlerRules);
            }
        }      

        public  async Task DeleteSharedData()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                await tenantService.DeleteAllAsync();
            }
        }

        public async Task DeleteTenantData(string tenantId)
        {
            var tenantService = _serviceProvider.GetRequiredService<ITenantService>();
            var tenant = await tenantService.GetByIdAsync(tenantId);

            using (var tenantScope = _serviceProvider.CreateScope())
            {
                // Set correct tenant for DI
                ITenantDatabaseConfig tenantDatabaseConfig = new TenantDatabaseConfig()
                {
                    TenantId = tenant.Id,
                    ConnectionString = tenant.ConnectionString,
                    DatabaseName = tenant.DatabaseName
                };
                var currentTenantContext = tenantScope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                currentTenantContext.TenantDatabaseConfig = tenantDatabaseConfig;

                var apiKeyService = tenantScope.ServiceProvider.GetRequiredService<IAPIKeyService>();
                var documentTemplateService = tenantScope.ServiceProvider.GetRequiredService<IDocumentTemplateService>();
                var eventClientService = tenantScope.ServiceProvider.GetRequiredService<IEventClientService>();
                var eventHandlerService = tenantScope.ServiceProvider.GetRequiredService<IEventHandlerService>();
                var eventHandlerRuleService = tenantScope.ServiceProvider.GetRequiredService<IEventHandlerRuleService>();
                var eventTypeService = tenantScope.ServiceProvider.GetRequiredService<IEventTypeService>();

                var consoleSettingsService = tenantScope.ServiceProvider.GetRequiredService<IConsoleSettingsService>();
                var csvSettingsService = tenantScope.ServiceProvider.GetRequiredService<ICSVSettingsService>();
                var emailSettingsService = tenantScope.ServiceProvider.GetRequiredService<IEmailSettingsService>();
                var httpSettingsService = tenantScope.ServiceProvider.GetRequiredService<IHTTPSettingsService>();
                var processSettingsService = tenantScope.ServiceProvider.GetRequiredService<IProcessSettingsService>();
                var signalRSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISignalRSettingsService>();
                var smsSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISMSSettingsService>();
                var sqlSettingsService = tenantScope.ServiceProvider.GetRequiredService<ISQLSettingsService>();
                var teamsSettingsService = tenantScope.ServiceProvider.GetRequiredService<ITeamsSettingsService>();

                // Base data
                await apiKeyService.DeleteAllAsync();
                await documentTemplateService.DeleteAllAsync();
                await eventClientService.DeleteAllAsync();
                await eventHandlerService.DeleteAllAsync();
                await eventTypeService.DeleteAllAsync();

                // Event settings
                await consoleSettingsService.DeleteAllAsync();
                await csvSettingsService.DeleteAllAsync();
                await emailSettingsService.DeleteAllAsync();
                await httpSettingsService.DeleteAllAsync();
                await processSettingsService.DeleteAllAsync();
                await smsSettingsService.DeleteAllAsync();
                await sqlSettingsService.DeleteAllAsync();
                await teamsSettingsService.DeleteAllAsync();

                await eventHandlerRuleService.DeleteAllAsync();
            }
        }

        public async Task DeleteAllData()
        {
            await DeleteSharedData();
                        
            // Delete tenant data
            var tenants = _tenantService.GetAll();
            foreach (var tenant in tenants)
            {
                await DeleteTenantData(tenant.Id);                
            }

            int xxx = 1000;
        }

        public async Task LoadSharedData(int group)
        {
            ISharedSeedDataService seedDataService = new SharedSeedDataService();

            // Get shared data
            var sharedSeed = seedDataService.GetSeedData(group);
          
            // Load to main DB
            using (var scope = _serviceProvider.CreateScope())
            {
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                await tenantService.ImportAsync(sharedSeed.Tenants);
            }
        }                          
    }
}
