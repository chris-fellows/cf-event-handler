using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System.IO;

namespace CFEventHandler.Process
{
    /// <summary>
    /// Handles event by running process
    /// </summary>
    public class ProcessEventHandler : IEventHandler
    {
        private readonly IProcessSettingsService _processSettingsService;

        public string Id => "Process";

        public ProcessEventHandler(IProcessSettingsService processSettingsService)
        {
            _processSettingsService = processSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _processSettingsService.GetByIdAsync(eventSettingsId).Result;
            
            if (!File.Exists(eventSettings.PathToProcess))
            {
                throw new ArgumentException($"File {eventSettings.PathToProcess} does not exist");
            }
        }
    }
}
