using System.Collections.Generic;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Rule for event handler to use for event
    /// 
    /// TODO: Enhance this later to be able to filter other event properties
    /// </summary>
    public class EventHandlerRule
    {
        /// <summary>
        /// Event type 
        /// </summary>
        public string EventTypeId { get; set; }

        /// <summary>
        /// Event handlers for this event type
        /// </summary>
        public List<string> EventHandlerIds { get; set; }
    }
}
