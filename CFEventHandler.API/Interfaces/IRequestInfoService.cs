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
        /// API key in request, not necessarily valid
        /// </summary>
        string? APIKey { get; }

        /// <summary>
        /// Details for valid API key in request. Will be null if no API key or invalid API key
        /// </summary>
        APIKeyInstance? ValidAPIKeyInstance { get; }

        /// <summary>
        /// Throws exception if tenant unknown. It would typically be called at the start of processing a request where
        /// data for a specific tenant must be accessed. Wouldn't be necessary for methods that are protected by a
        /// role requirement.
        /// </summary>
        void ThrowExceptionIfUnknownTenant();
    }
}
