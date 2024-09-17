using CFEventHandler.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Event handler
    /// </summary>
    public class EventHandler
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public EventHandlerTypes EventHandlerType { get; set; }
    }
}
