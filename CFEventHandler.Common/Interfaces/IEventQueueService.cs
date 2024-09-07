using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    public interface IEventQueueService
    {
        void Add(EventInstance eventInstance);

        EventInstance? GetNext();
    }
}
