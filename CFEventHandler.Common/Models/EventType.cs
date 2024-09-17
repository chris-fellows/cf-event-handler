using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Event type
    /// </summary>
    public class EventType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;
    }
}
