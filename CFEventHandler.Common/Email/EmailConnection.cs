namespace CFEventHandler.Email
{
    /// <summary>
    /// Email connection details
    /// 
    /// TODO: Consider storing in a collection so that instance can be shared between EmailEventSettings instances
    /// </summary>
    public class EmailConnection
    {
        public string Server { get; set; } = String.Empty;

        public int Port { get; set; } = 0;

        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;
    }
}
