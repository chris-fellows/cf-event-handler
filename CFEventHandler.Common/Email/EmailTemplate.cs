namespace CFEventHandler.Common.Email
{
    /// <summary>
    /// Email template
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Template content
        /// </summary>
        public byte[] Content { get; set; } = new byte[0];
    }
}
