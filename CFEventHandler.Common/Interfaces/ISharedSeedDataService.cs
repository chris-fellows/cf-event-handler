using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Seed data service for shared data
    /// </summary>
    public interface ISharedSeedDataService
    {      
        SharedSeed GetSeedData(int group);
    }
}
