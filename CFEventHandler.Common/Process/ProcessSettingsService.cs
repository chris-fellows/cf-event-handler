using CFEventHandler.HTTP;
using CFEventHandler.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Common.Process
{
    public class ProcessSettingsService : IProcessSettingsService
    {
        public async Task<List<ProcessEventSettings>> GetAllAsync()
        {
            return new List<ProcessEventSettings>();
        }

        public async Task<ProcessEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<ProcessEventSettings> AddAsync(ProcessEventSettings eventSettings)
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
