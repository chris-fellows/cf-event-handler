using CFEventHandler.Models;
using System;
using System.Collections.Generic;

namespace CFEventHandler.HTTP
{
    /// <summary>
    /// Settings for handling event for log to HTTP
    /// </summary>
    public class HTTPEventSettings : EventSettings
    {
        public string URL { get; set; } = String.Empty;

        public string Method { get; set; } = String.Empty;

        public Dictionary<string, string> Headers { get; set; }
    }
}
