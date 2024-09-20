using CFEventHandler.Interfaces;

namespace CFEventHandler.Services
{
    public class SecurityAdmin : ISecurityAdmin
    {
        private readonly IAPIKeyCacheService _apiKeyCacheService;
        private readonly IAPIKeyService _apiKeyService;

        public SecurityAdmin(IAPIKeyCacheService apiKeyCacheService,
                            IAPIKeyService apiKeyService)
        {
            _apiKeyCacheService = apiKeyCacheService;
            _apiKeyService = apiKeyService;
        }

        public void RefreshAPIKeyCache()
        {           
            _apiKeyCacheService.DeleteAll();

            var apiKeys = _apiKeyService.GetAll().ToList();
            apiKeys.ForEach(apiKey => _apiKeyCacheService.Add(apiKey, apiKey.Key));           
        }
    }
}
