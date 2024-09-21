using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    public interface ITenantSeedDataService   
    {        
        /// <summary>            
        /// Gets tenant data seed
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        TenantSeed GetSeedData(int group);
    }
}
