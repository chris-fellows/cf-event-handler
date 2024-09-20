using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    public interface ITenantService
    {
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ImportAsync(IEntityList<Tenant> tenantList);

        /// <summary>
        /// Exports to list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ExportAsync(IEntityList<Tenant> tenantList);

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tenant> GetAll();

        /// <summary>
        /// Gets tenant by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Tenant?> GetByIdAsync(string id);

        /// <summary>
        /// Gets tenant by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Tenant?> GetByNameAsync(string name);

        /// <summary>
        /// Adds tenant
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<Tenant> AddAsync(Tenant tenant);

        /// <summary>
        /// Deletes all tenants
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes tenant by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);
    }
}
