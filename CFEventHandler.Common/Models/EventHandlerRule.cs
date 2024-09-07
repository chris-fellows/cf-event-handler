namespace CFEventHandler.Models
{
    /// <summary>
    /// Rule for event handler to use for event    
    /// </summary>
    public class EventHandlerRule
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Displayable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether rule is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Event type that rule is valid for
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Event handler to use. E.g. SQL
        /// </summary>
        public string EventHandlerId { get; set; } = String.Empty;

        /// <summary>
        /// Event settings to handle when handling. E.g. Settings for SQL database
        /// </summary>
        public string EventSettingsId { get; set; } = String.Empty;
    }
}
