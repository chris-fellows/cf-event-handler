﻿using CFEventHandler.Interfaces;
using CFEventHandler.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.SMS
{
    public interface ISMSSettingsService : IEventSettingsService<SMSEventSettings>
    {
    }
}
