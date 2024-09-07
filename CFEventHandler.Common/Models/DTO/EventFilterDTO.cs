namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event filter
    /// </summary>
    public class EventFilterDTO
    {
        /// <summary>
        /// Event types filter
        /// </summary>
        public List<string> EventTypeIds { get; set; }

        /// <summary>
        /// Events created after date and time
        /// </summary>
        public DateTimeOffset FromCreatedDateTime { get; set; }

        /// <summary>
        /// Events created before date and time
        /// </summary>
        public DateTimeOffset ToCreatedDateTime { get; set; }
    }
}
