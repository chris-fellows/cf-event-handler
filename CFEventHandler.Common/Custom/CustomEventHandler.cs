﻿using CFEventHandler.Console;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Custom
{
    public class CustomEventHandler : IEventHandler
    {
        private readonly ICustomSettingsService _customSettingsService;

        public string Id => "Custom";

        public CustomEventHandler(ICustomSettingsService customSettingsService)
        {
            _customSettingsService = customSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _customSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
