using CFEventHandler.API.Interfaces;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.API.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IAPIKeyCacheService _apiKeyCacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ITenantCacheService _tenantCacheService;

        public RequestInfoService(IAPIKeyCacheService apiKeyCacheService,
                                 IHttpContextAccessor httpContextAccessor)                                 
        {
            _apiKeyCacheService = apiKeyCacheService;
            _httpContextAccessor = httpContextAccessor;
            //_tenantCacheService = tenantCacheService;                       
        }

        public string TenantId
        {
            get
            {
                var apiKeyInstance = this.APIKeyInstance;
                return apiKeyInstance == null ? null : apiKeyInstance.TenantId;
            }
        }

        public APIKeyInstance? APIKeyInstance
        {
            get
            {
                string apiKey = null;
                if (_httpContextAccessor.HttpContext != null &&
                    _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Api-Key", 
                            out var apiKeyHeaderValues)) apiKey = apiKeyHeaderValues.FirstOrDefault();

                if (!String.IsNullOrEmpty(apiKey))
                {
                    var apiKeyInstance = _apiKeyCacheService.GetById(apiKey);
                    return apiKeyInstance;
                }
                return null;
            }
        }
    }
}
