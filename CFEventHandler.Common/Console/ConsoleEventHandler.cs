using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Console
{
    /// <summary>
    /// Handles event by logging to console
    /// </summary>
    public class ConsoleEventHandler : IEventHandler
    {
        private readonly IConsoleSettingsService _consoleSettingsService;

        public string Id => "Console";

        public ConsoleEventHandler(IConsoleSettingsService consoleSettingsService)
        {
            _consoleSettingsService = consoleSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _consoleSettingsService.GetByIdAsync(eventSettingsId).Result;

            System.Console.WriteLine($"{eventInstance.CreatedDateTime} {eventInstance.EventTypeId}");
        }
    }
}
