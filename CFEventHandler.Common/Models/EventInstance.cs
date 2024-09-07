namespace CFEventHandler.Models
{
    /// <summary>
    /// Event instance
    /// </summary>
    public class EventInstance
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Event type
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Time created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Parameters
        /// </summary>
        public List<EventParameter> Parameters { get; set; }
    }
}
