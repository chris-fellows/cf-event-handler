using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Common.Seed
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
                EventHandlerId = "1",
                EventSettingsId = "1",
                Name = "Event handler rule 1"
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "2",
                EventTypeId = "2",
                EventHandlerId = "2",
                EventSettingsId = "2",
                Name = "Event handler rule 2"
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "3",
                EventTypeId = "3",
                EventHandlerId = "3",
                EventSettingsId = "3",
                Name = "Event handler rule 3"
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "4",
                EventTypeId = "4",
                EventHandlerId = "4",
                EventSettingsId = "4",
                Name = "Event handler rule 4"
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "5",
                EventTypeId = "5",
                EventHandlerId = "5",
                EventSettingsId = "5",
                Name = "Event handler rule 5"
            });

            return eventHandlerRules;
        }

        public async Task WriteAllAsync(List<EventHandlerRule> eventHandlerRules)
        {
            // No action
        }
    }
}
