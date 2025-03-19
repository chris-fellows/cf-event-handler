using CFEventHandler.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;

namespace CFEventHandler.API.HealthCheck
{
    /// <summary>
    /// Database connection health check
    /// </summary>
    public class DatabaseConnectionHealthCheck : IHealthCheck
    {
        private readonly IEventTypeService _eventTypeService;

        public DatabaseConnectionHealthCheck(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
                            CancellationToken cancellationToken = default)
        {
            StringBuilder data = new StringBuilder("");

            try
            {

                var eventTypes = _eventTypeService.GetAll();
            }
            catch(Exception exception)
            {
                if (data.Length > 0) data.Append("; ");
                data.Append($"Database connection error: {exception.Message}");
            }           

            if (data.Length == 0)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("Database connection is healthy"));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, data.ToString()));
        }
    }
}
