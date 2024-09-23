using CFEventHandler.Exceptions;
using CFEventHandler.API.Interfaces;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.API.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IAPIKeyCacheService _apiKeyCacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;        

        public RequestInfoService(IAPIKeyCacheService apiKeyCacheService,
                                 IHttpContextAccessor httpContextAccessor)                                 
        {
            _apiKeyCacheService = apiKeyCacheService;
            _httpContextAccessor = httpContextAccessor;                                 
        }

        public string? TenantId
        {
            get
            {
                var apiKeyInstance = this.ValidAPIKeyInstance;
                return apiKeyInstance == null ? null : apiKeyInstance.TenantId;
            }
        }

        public string? APIKey
        {
            get
            {
                string apiKey = null;
                if (_httpContextAccessor.HttpContext != null &&
                    _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Api-Key",
                            out var apiKeyHeaderValues)) apiKey = apiKeyHeaderValues.FirstOrDefault();
                return apiKey;
            }
        }

        public APIKeyInstance? ValidAPIKeyInstance
        {
            get
            {
                var apiKey = APIKey;              
                if (!String.IsNullOrEmpty(apiKey))
                {
                    var apiKeyInstance = _apiKeyCacheService.GetById(apiKey);
                    return apiKeyInstance;
                }
                return null;
            }
        }

        public void ThrowExceptionIfUnknownTenant()
        {
            if (String.IsNullOrEmpty(TenantId))
            {
                throw new GeneralException("Tenant is unknown");
            }
        }
    }
}
