using CFEventHandler.Console;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.CSV
{
    public class CSVSettingsService : ICSVSettingsService
    {
        public async Task<List<CSVEventSettings>> GetAllAsync()
        {
            return new List<CSVEventSettings>();
        }

        public async Task<CSVEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<CSVEventSettings> AddAsync(CSVEventSettings eventSettings)
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
