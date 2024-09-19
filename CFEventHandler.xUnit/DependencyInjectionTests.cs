using AutoMapper;
using CFEventHandler.API.Interfaces;
using CFEventHandler.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.xUnit
{
    /// <summary>
    /// Tests for dependency injection
    /// </summary>
    public class DependencyInjectionTests : IClassFixture<WebApplicationFactory<Program>>    
    { 
        private readonly WebApplicationFactory<Program> _factory;

        public DependencyInjectionTests(WebApplicationFactory<Program> factory)
        {
            // Set local appsettings.json
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Test.json");
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });
        }
        
        [Theory]        
        [InlineData(typeof(IAPIKeyCacheService))]
        [InlineData(typeof(IAPIKeyService))]
        [InlineData(typeof(IDatabaseAdmin))]
        [InlineData(typeof(IDatabaseConfig))]
        [InlineData(typeof(IEventClientService))]
        [InlineData(typeof(IEventManagerService))]
        [InlineData(typeof(IEventQueueService))]
        [InlineData(typeof(IEventService))]
        [InlineData(typeof(IEventTypeService))]
        [InlineData(typeof(IMapper))]
        [InlineData(typeof(IRequestInfoService))]
        [InlineData(typeof(ISecurityAdmin))]
        public async Task Get_Registered_Service_Succeeds(Type serviceType)
        {            
            using (var scope = _factory.Services.CreateScope())
            {                
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);                
            }            
        }
    }
}
