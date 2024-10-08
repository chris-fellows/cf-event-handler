﻿namespace CFEventHandler.Models
{
    /// <summary>
    /// Event instance filter
    /// </summary>
    public class EventFilter
    {
        public List<string>? EventTypeIds { get; set; }

        public List<string>? EventClientIds { get; set; }

        public DateTimeOffset FromCreatedDateTime { get; set; }

        public DateTimeOffset ToCreatedDateTime { get; set; }

        public int PageNo { get; set; }

        public int PageItems { get; set; }

        public bool IsValidForFilter(EventInstance eventInstance)
        {
            return true;
        }
    }
}
