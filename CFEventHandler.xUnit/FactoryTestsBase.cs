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
    /// Base class for factory tests
    /// </summary>
    public abstract class FactoryTestsBase
    {
        protected readonly WebApplicationFactory<Program> _factory;

        public FactoryTestsBase(WebApplicationFactory<Program> factory)
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
    }
}
