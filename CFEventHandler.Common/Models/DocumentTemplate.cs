namespace CFEventHandler.Models
{
    /// <summary>
    /// Document template. E.g. Template for email, HTTP content, MS Teams message   
    /// </summary>
    public class DocumentTemplate
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Template content. May contain placeholders that are replaced at runtime, typically from event parameters.
        /// </summary>
        public byte[] Content { get; set; } = new byte[0];

        //public List<DocumentParameter> Parameters { get; set; }
    }
}
