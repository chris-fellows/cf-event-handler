﻿using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Interfaces
{
    public interface IEventClientService
    {
        /// <summary>
        /// Imports from list
        /// </summary>
        /// <param name="eventClientList"></param>
        /// <returns></returns>
        Task ImportAsync(IEntityList<EventClient> eventClientList);

        /// <summary>
        /// Exports to list
        /// </summary>
        /// <param name="eventTypeList"></param>
        /// <returns></returns>
        Task ExportAsync(IEntityList<EventClient> eventClientList);

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns></returns>
        IEnumerable<EventClient> GetAll();

        /// <summary>
        /// Gets event client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EventClient?> GetByIdAsync(string id);

        /// <summary>
        /// Adds event client
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventClient> AddAsync(EventClient eventClient);

        /// <summary>
        /// Deletes all event types
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Deletes event type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);
    }
}
