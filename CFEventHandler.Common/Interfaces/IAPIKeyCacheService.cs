using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for API key cache
    /// </summary>
    public interface IAPIKeyCacheService : ICacheService<APIKeyInstance, string>
    {
    }
}
