using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for event settings
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IEventSettingsService<TEntityType>
    {
        Task<List<TEntityType>> GetAllAsync();
        
        Task<TEntityType> GetByIdAsync(string id);

        Task<TEntityType> AddAsync(TEntityType entityType);

        Task DeleteAllAsync();

        Task DeleteByIdAsync(string id);
    }
}
