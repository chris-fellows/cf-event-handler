using CFEventHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Interfaces
{
    public interface ITenantDatabaseConfig : IDatabaseConfig
    {
        string TenantId { get; }
    }
}
