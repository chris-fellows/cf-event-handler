using CFEventHandler.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.xUnit
{
    public class ControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        //private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ControllerTests(WebApplicationFactory<Program> factory)
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

        [Fact]
        public async Task TestMeWorks()
        {
            // Test that correct appsettings.json is used
            using (var scope = _factory.Services.CreateScope())
            {
                IDatabaseConfig databaseConfig = scope.ServiceProvider.GetRequiredService<IDatabaseConfig>();
                Assert.Equal("event_handler_test", databaseConfig.DatabaseName);

                int xxxx = 1000;
            }

            //Act
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/Me");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            int xxx = 1000;
        }
    }
}
