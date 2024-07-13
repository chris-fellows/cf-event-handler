using System;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Event parameter
    /// </summary>
    public class EventParameter
    {
        public string Name { get; set; } = String.Empty;

        public object Value { get; set; } = null;
    }
}
