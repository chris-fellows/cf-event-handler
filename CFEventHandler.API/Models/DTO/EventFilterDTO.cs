namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event filter DTO
    /// </summary>
    public class EventFilterDTO
    {
        /// <summary>
        /// Event types filter (if any)
        /// </summary>
        public List<string>? EventTypeIds { get; set; }

        /// <summary>
        /// Event clients filter (if any)
        /// </summary>
        public List<string>? EventClientIds { get; set; }

        /// <summary>
        /// Events created after date and time
        /// </summary>
        public DateTimeOffset FromCreatedDateTime { get; set; }

        /// <summary>
        /// Events created before date and time
        /// </summary>
        public DateTimeOffset ToCreatedDateTime { get; set; }

        /// <summary>
        /// Page number (1 or more)
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Page items (1 or more)
        /// </summary>
        public int PageItems { get; set; }
    }
}
