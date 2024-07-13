using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    public interface IEventManagerService
    {
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="eventInstance"></param>
        void Handle(EventInstance eventInstance);
    }
}
