using CFEventHandler.Custom;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System.Collections.Generic;

namespace CFEventHandler.Email
{
    public class EmailSettingsService : IEmailSettingsService
    {        
        public async Task<List<EmailEventSettings>> GetAllAsync()
        {
            return new List<EmailEventSettings>();
        }

        public async Task<EmailEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<EmailEventSettings> AddAsync(EmailEventSettings eventSettings)
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
