using CFEventHandler.Enums;
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
                EventHandlerId = "Email",
                EventSettingsId = "Email1",
                Name = "Event handler rule 1",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        ItemName = "CompanyId",
                        ConditionType = ConditionTypes.Equals,
                        Values = new List<object>() { 1 }                        
                    }
                }
            });

            eventHandlerRules.Add(new EventHandlerRule()
            {
                Id = "2",
                EventTypeId = "2",
                EventHandlerId = "Email",
                EventSettingsId = "Email2",
                Name = "Event handler rule 2",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        ItemName = "CompanyId",
                        ConditionType = ConditionTypes.Equals,
                        Values = new List<object>() { 2 }
                    }
                }
            });

            return eventHandlerRules;
        }

        public async Task WriteAllAsync(List<EventHandlerRule> eventHandlerRules)
        {
            // No action
        }
    }
}
