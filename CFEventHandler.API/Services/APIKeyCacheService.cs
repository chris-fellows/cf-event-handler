using CFEventHandler.API.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Services;

namespace CFEventHandler.API.Services
{
    public class APIKeyCacheService : CacheService<APIKeyInstance, string>, IAPIKeyCacheService
    {
    }
}
