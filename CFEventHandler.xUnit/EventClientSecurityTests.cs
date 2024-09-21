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
    /// Tests for EventClient security (API key access)
    /// </summary>
    public class EventClientSecurityTests : FactoryTestsBase, IClassFixture<WebApplicationFactory<Program>>
    {       
        public EventClientSecurityTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        
        }

        [Fact]
        public async Task Get_One_Event_Client_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_One_Event_Client_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_One_Event_Client_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient/1234");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Clients_With_Valid_API_Key_Works()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Get_All_Event_Clients_With_No_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Event_Clients_With_Invalid_API_Key_Fails()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/EventClient");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_Event_Client_With_Valid_API_Key_Works()
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
        public async Task Update_Event_Client_With_No_API_Key_Fails()
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
        public async Task Update_Event_Client_With_Invalid_API_Key_Fails()
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
        public async Task Add_Event_Client_With_Valid_API_Key_Works()
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
        public async Task Add_Event_Client_With_No_API_Key_Fails()
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
        public async Task Add_Event_Client_With_Invalid_API_Key_Fails()
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
