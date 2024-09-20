using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Services;

namespace CFEventHandler.Services
{
    public class APIKeyCacheService : CacheService<APIKeyInstance, string>, IAPIKeyCacheService
    {
    }
}
