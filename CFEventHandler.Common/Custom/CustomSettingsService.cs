using CFEventHandler.CSV;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Custom
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task<List<CustomEventSettings>> GetAllAsync()
        {
            return new List<CustomEventSettings>();
        }


        public async Task<CustomEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<CustomEventSettings> AddAsync(CustomEventSettings eventSettings)
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
