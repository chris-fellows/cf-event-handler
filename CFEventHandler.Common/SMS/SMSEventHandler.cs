using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.SMS
{
    public class SMSEventHandler : IEventHandler
    {
        private readonly ISMSSettingsService _smsSettingsService;

        public string Id => "SMS";

        public SMSEventHandler(ISMSSettingsService smsSettingsService)
        {
            _smsSettingsService = smsSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _smsSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
