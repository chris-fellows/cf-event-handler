using Microsoft.Extensions.Primitives;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for database admin
    /// </summary>
    public interface IDatabaseAdminService
    {
        /// <summary>
        /// Initialises shared database
        /// </summary>
        /// <returns></returns>
        Task InitialiseSharedAsync();

        /// <summary>
        /// Initialises tenant database
        /// </summary>
        /// <returns></returns>
        Task InitialiseTenantAsync(string tenantId);

        /// <summary>
        /// Deletes all data (Shared data and tenant data)
        /// </summary>
        /// <returns></returns>
        Task DeleteAllData();

        /// <summary>
        /// Deletes shared data
        /// </summary>
        /// <returns></returns>
        Task DeleteSharedData();

        /// <summary>
        /// Deletes data for tenant
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns></returns>
        Task DeleteTenantData(string tenantId);

        /// <summary>
        /// Loads shared data from specific group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task LoadSharedData(int group);

        /// <summary>
        /// Loads tenant data from specific group
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task LoadTenantData(string tenantId, int group);
    }
}
