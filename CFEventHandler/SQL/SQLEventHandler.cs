using CFEventHandler.Console;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.SQL
{
    public class SQLEventHandler : IEventHandler
    {
        private readonly ISQLSettingsService _sqlSettingsService;

        public string Id => typeof(ConsoleEventHandler).Name;

        public SQLEventHandler(ISQLSettingsService sqlSettingsService)
        {
            _sqlSettingsService = sqlSettingsService;
        }

        public void Handle(EventInstance eventInstance)
        {
            // TODO: Implement this
        }
    }
}
