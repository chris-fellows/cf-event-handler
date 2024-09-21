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
        public IEntityList<APIKeyInstance> APIKeys { get; set; }
        public IEntityList<DocumentTemplate> DocumentTemplates { get; set; }

        public IEntityList<EventClient> EventClients { get; set; }

        public IEntityList<CFEventHanderObject> EventHandlers { get; set; }

        public IEntityList<EventHandlerRule> EventHandlerRules { get; set; }

        public IEntityList<EventType> EventTypes { get; set; }    
        
        public IEntityList<ConsoleEventSettings> ConsoleEventSettings { get; set; }

        public IEntityList<CSVEventSettings> CSVEventSettings { get; set; }

        public IEntityList<EmailEventSettings> EmailEventSettings { get; set; }

        public IEntityList<HTTPEventSettings> HTTPEventSettings { get; set; }

        public IEntityList<ProcessEventSettings> ProcessEventSettings { get; set; }

        public IEntityList<SignalREventSettings> SignalREventSettings { get; set; }

        public IEntityList<SMSEventSettings> SMSEventSettings { get; set; }

        public IEntityList<SQLEventSettings> SQLEventSettings { get; set; }

        public IEntityList<TeamsEventSettings> TeamsEventSettings { get; set; }
    }
}
