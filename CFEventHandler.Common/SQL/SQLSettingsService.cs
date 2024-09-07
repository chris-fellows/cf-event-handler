using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.SQL
{
    public class SQLSettingsService : ISQLSettingsService
    {
        public async Task<List<SQLEventSettings>> GetAllAsync()
        {
            return new List<SQLEventSettings>();
        }

        public async Task<SQLEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<SQLEventSettings> AddAsync(SQLEventSettings eventSettings)
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
