using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.API.Interfaces
{
    /// <summary>
    /// Interface for API key cache
    /// </summary>
    public interface IAPIKeyCacheService : ICacheService<APIKeyInstance, string>
    {
    }
}
