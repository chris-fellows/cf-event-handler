using CFEventHandler.Console;
using CFEventHandler.CSV;
using CFEventHandler.Email;
using CFEventHandler.HTTP;
using CFEventHandler.Interfaces;
using CFEventHandler.Process;
using CFEventHandler.SignalR;
using CFEventHandler.SMS;
using CFEventHandler.SQL;
using CFEventHandler.Teams;
using CFEventHanderObject = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Seed data for tenant
    /// </summary>
    public class TenantSeed
    {
        public IEntityReader<APIKeyInstance> APIKeys { get; set; }
        public IEntityReader<DocumentTemplate> DocumentTemplates { get; set; }

        public IEntityReader<EventClient> EventClients { get; set; }

        public IEntityReader<CFEventHanderObject> EventHandlers { get; set; }

        public IEntityReader<EventHandlerRule> EventHandlerRules { get; set; }

        public IEntityReader<EventType> EventTypes { get; set; }    
        
        public IEntityReader<ConsoleEventSettings> ConsoleEventSettings { get; set; }

        public IEntityReader<CSVEventSettings> CSVEventSettings { get; set; }

        public IEntityReader<EmailEventSettings> EmailEventSettings { get; set; }

        public IEntityReader<HTTPEventSettings> HTTPEventSettings { get; set; }

        public IEntityReader<ProcessEventSettings> ProcessEventSettings { get; set; }

        public IEntityReader<SignalREventSettings> SignalREventSettings { get; set; }

        public IEntityReader<SMSEventSettings> SMSEventSettings { get; set; }

        public IEntityReader<SQLEventSettings> SQLEventSettings { get; set; }

        public IEntityReader<TeamsEventSettings> TeamsEventSettings { get; set; }
    }
}
