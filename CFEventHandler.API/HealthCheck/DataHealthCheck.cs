using CFEventHandler.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;

namespace CFEventHandler.HealthCheck
{
    /// <summary>
    /// Data health check
    /// </summary>
    public class DataHealthCheck : IHealthCheck
    {
        private readonly IEventTypeService _eventTypeService;

        public DataHealthCheck(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
                            CancellationToken cancellationToken = default)
        {
            StringBuilder data = new StringBuilder("");

            var eventTypes = _eventTypeService.GetAllAsync().Result;
            if (!eventTypes.Any())
            {
                if (data.Length > 0) data.Append("; ");
                data.Append("No event types in database");
            }            

            if (data.Length == 0)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("Data is healthy"));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, data.ToString()));
        }
    }
}
