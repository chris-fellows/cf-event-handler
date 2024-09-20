using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Common.Seed
{
    public class APIKeySeed1 : IEntityList<APIKeyInstance>
    {
        public async Task<List<APIKeyInstance>> ReadAllAsync()
        {
            var apiKeys = new List<APIKeyInstance>();

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 1",
                Key = "111111",
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "Admin"
                }
            });

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 2",
                Key = "222222",
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "Admin"
                }
            });

            return apiKeys;
        }

        public async Task WriteAllAsync(List<APIKeyInstance> apiKeys)
        {
            // No action            
        }
    }
}
