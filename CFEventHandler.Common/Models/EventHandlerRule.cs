using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Rule for event handler to use for event    
    /// </summary>
    public class EventHandlerRule
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Displayable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether rule is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Event type that rule is valid for
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Event handler to use. E.g. SQL
        /// </summary>
        public string EventHandlerId { get; set; } = String.Empty;

        /// <summary>
        /// Event settings to handle when handling. E.g. Settings for SQL database
        /// </summary>
        public string EventSettingsId { get; set; } = String.Empty;

        /// <summary>
        /// Conditions for rule applying
        /// </summary>
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        /// <summary>
        /// Whether event meets conditions for event handler rule
        /// </summary>
        /// <param name="eventInstance"></param>
        /// <param name="eventHandlerRule"></param>
        /// <returns></returns>
        public bool IsEventMeetsRuleConditions(EventInstance eventInstance)
        {
            if (!Conditions.Any()) return true;    // No conditions, valid

            // Get event parameter values to check            
            var valuesByItemName = new Dictionary<string, object>();
            valuesByItemName.Add(nameof(EventInstance.EventTypeId), eventInstance.EventTypeId);
            valuesByItemName.Add(nameof(EventInstance.EventClientId), eventInstance.EventClientId);
            foreach (var condition in Conditions)
            {
                if (!valuesByItemName.ContainsKey(condition.ItemName))   // Not already added
                {
                    var eventParameter = eventInstance.Parameters.FirstOrDefault(p => p.Name == condition.ItemName);
                    if (eventParameter == null)   // Event parameter not logged
                    {
                        valuesByItemName.Add(condition.ItemName, null);
                    }
                    else
                    {
                        valuesByItemName.Add(condition.ItemName, eventParameter.Value);
                    }
                }
            }

            return Condition.IsMeetsConditions(Conditions, valuesByItemName);
        }
    }
}
