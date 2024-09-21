namespace CFEventHandler.Models.DTO
{
    /// <summary>
    /// Event instance DTO
    /// </summary>
    public class EventInstanceDTO
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Event type
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Client that created event
        /// </summary>
        public string EventClientId { get; set; } = String.Empty;

        /// <summary>
        /// Time created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Parameters
        /// </summary>
        public List<EventParameterDTO> Parameters { get; set; }
    }
}
