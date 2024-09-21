using CFEventHandler.Models;

namespace CFEventHandler.API.Interfaces
{
    /// <summary>
    /// Interface for HTTP request details
    /// </summary>
    public interface IRequestInfoService
    {
        /// <summary>
        /// Tenant Id
        /// </summary>
        string? TenantId { get; }

        /// <summary>
        /// API key in request
        /// </summary>
        APIKeyInstance? APIKeyInstance { get; }
    }
}
