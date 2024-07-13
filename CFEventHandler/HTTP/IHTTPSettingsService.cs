﻿using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.HTTP
{
    public interface IHTTPSettingsService
    {
        HTTPEventSettings GetSettings(EventInstance eventInstance);
    }
}
