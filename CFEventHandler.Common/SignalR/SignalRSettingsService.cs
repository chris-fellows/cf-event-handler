using CFEventHandler.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Common.SignalR
{
    public class SignalRSettingsService : ISignalRSettingsService
    {
        public async Task<List<SignalREventSettings>> GetAllAsync()
        {
            return new List<SignalREventSettings>();
        }

        public async Task<SignalREventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<SignalREventSettings> AddAsync(SignalREventSettings eventSettings)
        {
            return null;
        }

        public async Task DeleteAllAsync()
        {

        }

        public async Task DeleteByIdAsync(string id)
        {

        }
    }
}
