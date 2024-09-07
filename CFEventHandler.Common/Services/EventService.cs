using CFEventHandler.Common.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Common.Services
{
    public class EventService : IEventService
    { 
        public async Task<EventInstance> AddAsync(EventInstance eventInstance)
        {
            return null;
        }
        
        public Task<List<EventInstance>> GetByFilter(EventFilter eventFilter)
        {
            return null;
        }
    }
}
