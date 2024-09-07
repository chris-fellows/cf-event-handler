using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Console
{
    public class ConsoleSettingsService : IConsoleSettingsService
    {
        public async Task<List<ConsoleEventSettings>> GetAllAsync()
        {
            return new List<ConsoleEventSettings>();
        }

        public async Task<ConsoleEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<ConsoleEventSettings> AddAsync(ConsoleEventSettings eventSettings)
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
