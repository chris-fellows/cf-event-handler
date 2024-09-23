using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.xUnit
{
    public class EventSummaryTests1 : FactoryTestsBase, IClassFixture<WebApplicationFactory<Program>>
    {
        public EventSummaryTests1(WebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Fact]
        public async Task Get_Event_Summary_Returns_Summarised_Events()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                // Get tenant to use
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var tenants = tenantService.GetAll();
                var tenant = tenants.Last();    // Use last incase code defaults to first

                // Set current tenant context
                var currentTenantContext = scope.ServiceProvider.GetRequiredService<ICurrentTenantContext>();
                currentTenantContext.TenantDatabaseConfig = new TenantDatabaseConfig()
                {
                    TenantId = tenant.Id,
                    DatabaseName = tenant.DatabaseName,
                    ConnectionString = tenant.ConnectionString
                };

                var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();

                var eventFilter = new EventFilter()
                {
                    FromCreatedDateTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(-90)),
                    ToCreatedDateTime = DateTimeOffset.UtcNow.AddDays(1),                    
                    PageItems = 10000000,
                    PageNo = 1
                };

                var eventSummaries = await eventService.GetEventSummary(eventFilter);
                int xxx = 1000;
            }
        }
    }
}

