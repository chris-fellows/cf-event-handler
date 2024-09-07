namespace CFEventHandler.Teams
{
    public class TeamsSettingsService : ITeamsSettingsService
    {
        public async Task<List<TeamsEventSettings>> GetAllAsync()
        {
            return new List<TeamsEventSettings>();
        }

        public async Task<TeamsEventSettings> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task<TeamsEventSettings> AddAsync(TeamsEventSettings eventSettings)
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
