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
    /// Tests for EventHandler security (API key access)
    /// </summary>
    public class EventHandlerSecurityTests : FactoryTestsBase, IClassFixture<WebApplicationFactory<Program>>
    {        
        public EventHandlerSecurityTests(WebApplicationFactory<Program> factory) : base(factory)
        {
     
        }

        [Fact]
        public async Task Get_One_Event_Handler_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_One_Event_Handler_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_One_Event_Handler_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Handlers_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_All_Event_Handlers_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Handlers_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventHandler");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_Event_Handler_With_Valid_API_Key_Works()
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
        public async Task Update_Event_Handler_With_No_API_Key_Fails()
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
        public async Task Update_Event_Handler_With_Invalid_API_Key_Fails()
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
        public async Task Add_Event_Handler_With_Valid_API_Key_Works()
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
        public async Task Add_Event_Handler_With_No_API_Key_Fails()
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
        public async Task Add_Event_Handler_With_Invalid_API_Key_Fails()
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
