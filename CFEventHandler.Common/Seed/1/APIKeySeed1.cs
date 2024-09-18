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
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "XXX",
                    "Role1"
                }
            });

            apiKeys.Add(new APIKeyInstance()
            {
                Name = "API Key 2",
                Key = "222222",
                EndTime = DateTimeOffset.UtcNow.AddDays(180),
                Roles = new List<string>()
                {
                    "XXX",
                    "Role2"
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
