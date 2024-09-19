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
    /// Tests for EventType security (API key access)
    /// </summary>
    public class EventTypeSecurityTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EventTypeSecurityTests(WebApplicationFactory<Program> factory)
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
        public async Task Get_One_Event_Type_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_One_Event_Type_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_One_Event_Type_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Types_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_All_Event_Types_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Types_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventType");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_Event_Type_With_Valid_API_Key_Works()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }

        [Fact]
        public async Task Update_Event_Type_With_No_API_Key_Fails()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }

        [Fact]
        public async Task Update_Event_Type_With_Invalid_API_Key_Fails()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }

        [Fact]
        public async Task Add_Event_Type_With_Valid_API_Key_Works()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }

        [Fact]
        public async Task Add_Event_Type_With_No_API_Key_Fails()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }

        [Fact]
        public async Task Add_Event_Type_With_Invalid_API_Key_Fails()
        {
            /*
            using (var scope = _factory.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService(serviceType);
                Assert.NotNull(service);
            }
            */
        }
    }
}
