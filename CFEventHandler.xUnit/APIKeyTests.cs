using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.xUnit
{
    /// <summary>
    /// Tests for API keys
    /// </summary>
    public class APIKeyTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public APIKeyTests(WebApplicationFactory<Program> factory)
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
        public async Task No_API_Key_For_Anonymous_Method_Returns_OK()
        {            
            //Act
            var client = _factory.CreateClient();            
            var response = await client.GetAsync("/Test/APIKey/None");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            int xxx = 1000;
        }

        [Fact]
        public async Task API_Key_Invalid_Returns_Unauthorized()
        {
            //// Test that correct appsettings.json is used
            //using (var scope = _factory.Services.CreateScope())
            //{
            //    IDatabaseConfig databaseConfig = scope.ServiceProvider.GetRequiredService<IDatabaseConfig>();
            //    Assert.Equal("event_handler_test", databaseConfig.DatabaseName);

            //    int xxxx = 1000;
            //}

            var apiKey = "API_KEY_IS_INVALID";

            //Act
            var client = _factory.CreateClient();                        
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            var response = await client.GetAsync("/Test/APIKey/Roles/Admin");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);

            int xxx = 1000;
        }

        [Fact]
        public async Task No_API_Key_Returns_Unauthorized()
        {          
            //Act
            var client = _factory.CreateClient();            
            var response = await client.GetAsync("/Test/APIKey/Roles/Admin");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);

            int xxx = 1000;
        }

        [Fact]
        public async Task API_Key_With_Admin_Role_Returns_OK()
        {        
            var apiKey = "111111";  // Valid key

            //Act
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            var response = await client.GetAsync("/Test/APIKey/Roles/Admin");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            int xxx = 1000;
        }

        [Fact]
        public async Task API_Key_Without_Admin_Returns_Forbidden()
        {          
            var apiKey = "222222";  // Valid key but role missing

            //Act
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            var response = await client.GetAsync("/Test/APIKey/Roles/ReadEvent");
            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);

            int xxx = 1000;
        }
    }
}
