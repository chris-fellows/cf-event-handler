using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    public class APIKeyCacheService : CacheService<APIKeyInstance, string>, IAPIKeyCacheService
    {
    }
}
