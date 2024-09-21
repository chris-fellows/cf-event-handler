using CFEventHandler.Common.Seed;
using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using CFEventHandler.Seed;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;

namespace CFEventHandler.Services
{
    public class SharedSeedDataService : ISharedSeedDataService
    { 
        public SharedSeed GetSeedData(int group)
        {
            var sharedSeed = new SharedSeed();

            switch (group)
            {
                case 1:
                    sharedSeed.Tenants = new TenantSeed1();
                    break;
            }

            return sharedSeed;
        }      
    }
}
