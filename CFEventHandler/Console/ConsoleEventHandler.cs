using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Console
{
    /// <summary>
    /// Handles event by logging to console
    /// </summary>
    public class ConsoleEventHandler : IEventHandler
    {
        private readonly IConsoleSettingsService _consoleSettingsService;

        public string Id => typeof(ConsoleEventHandler).Name;

        public ConsoleEventHandler(IConsoleSettingsService consoleSettingsService)
        {
            _consoleSettingsService = consoleSettingsService;
        }

        public void Handle(EventInstance eventInstance)
        {
            System.Console.WriteLine($"{eventInstance.CreatedDateTime} {eventInstance.EventTypeId}");
        }
    }
}
