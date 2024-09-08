using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Seed
{
    public class EventHandlerRuleSeed1 : IEntityList<EventHandlerRule>
    {
        public async Task<List<EventHandlerRule>> ReadAllAsync()
        {
            var eventHandlerRules = new List<EventHandlerRule>();

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "1",
                EventTypeId = "1",                
                EventHandlerId = "5",
                EventSettingsId = "Email1",
                Name = "Event handler rule 1"
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "2",
                EventTypeId = "2",
                EventHandlerId = "5",
                EventSettingsId = "Email2",
                Name = "Event handler rule 2"
            });

            return eventHandlerRules;
        }

        public async Task WriteAllAsync(List<EventHandlerRule> eventHandlerRules)
        {
            // No action
        }
    }
}
