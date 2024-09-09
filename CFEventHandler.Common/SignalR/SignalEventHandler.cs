using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.SignalR
{
    /// <summary>
    /// Handles event by SignalR message
    /// </summary>
    public class SignalEventHandler : IEventHandler
    {
        private readonly ISignalRSettingsService _signalRSettingsService;

        public string Id => "SignalR";

        public SignalEventHandler(ISignalRSettingsService signalRSettingsService)
        {
            _signalRSettingsService = signalRSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _signalRSettingsService.GetByIdAsync(eventSettingsId).Result;           
        }
    }
}
