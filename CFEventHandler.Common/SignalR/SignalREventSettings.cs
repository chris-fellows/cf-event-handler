using CFEventHandler.Models;

namespace CFEventHandler.Common.SignalR
{
    public class SignalREventSettings : EventSettings
    {
        /// <summary>
        /// SignalR method name
        /// </summary>
        public string Method { get; set; } = String.Empty;

        /// <summary>
        /// SignalR hub URL
        /// </summary>
        public string HubUrl { get; set; } = String.Empty;
    }
}
