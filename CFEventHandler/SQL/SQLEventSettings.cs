using CFEventHandler.Models;
using System;

namespace CFEventHandler.SQL
{
    /// <summary>
    /// Settings for handling event for log to SQL
    /// </summary>
    public class SQLEventSettings : EventSettings
    {
        public string ConnectionString { get; set; }
    }
}
