using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Handles event
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Handler Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="eventInstance"></param>
        /// <param name="eventSettingsId"></param>
        void Handle(EventInstance eventInstance, string eventSettingsId);
    }
}
