namespace CFEventHandler.Models
{
    /// <summary>
    /// Event parameter
    /// </summary>
    public class EventParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Parameter value
        /// </summary>
        public object Value { get; set; } = null;
    }
}
