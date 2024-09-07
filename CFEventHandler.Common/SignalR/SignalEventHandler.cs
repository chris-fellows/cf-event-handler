using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using CFEventHandler.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Common.SignalR
{
    /// <summary>
    /// Handles event by SignalR message
    /// </summary>
    public class SignalEventHandler : IEventHandler
    {
        private readonly ISignalRSettingsService _signalRSettingsService;

        public string Id => typeof(SignalEventHandler).Name;

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
