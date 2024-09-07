using CFEventHandler.Email;

namespace CFEventHandler.HTTP
{
    public class HTTPSettingsService : IHTTPSettingsService
    {
        public async Task<List<HTTPEventSettings>> GetAllAsync()
        {
            return new List<HTTPEventSettings>();
        }

        public async Task<HTTPEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<HTTPEventSettings> AddAsync(HTTPEventSettings eventSettings)
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
