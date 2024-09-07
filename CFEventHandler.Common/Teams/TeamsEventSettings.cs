using CFEventHandler.Models;

namespace CFEventHandler.Teams
{
    /// <summary>
    /// Settings for handling event for log to MS Teams
    /// </summary>
    public class TeamsEventSettings : EventSettings
    {
        public string URL { get; set; }
    }
}
