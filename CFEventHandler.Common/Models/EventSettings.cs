namespace CFEventHandler.Models
{
    /// <summary>
    /// Settings for handling event
    /// </summary>
    public abstract class EventSettings
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Displayable name
        /// </summary>
        public string Name { get; set; } = String.Empty;
    }
}
