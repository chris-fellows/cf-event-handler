﻿using CFEventHandler.Models;

namespace CFEventHandler.SQL
{
    /// <summary>
    /// Settings for handling event for log to SQL
    /// </summary>
    public class SQLEventSettings : EventSettings
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// SQL template. E.g. SP name. May contain placeholders.
        /// </summary>
        public string TemplateSQL { get; set; }
    }
}
