using CFEventHandler.Console;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.SQL
{
    public class SQLEventHandler : IEventHandler
    {
        private readonly ISQLSettingsService _sqlSettingsService;

        public string Id => "SQL";

        public SQLEventHandler(ISQLSettingsService sqlSettingsService)
        {
            _sqlSettingsService = sqlSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _sqlSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
