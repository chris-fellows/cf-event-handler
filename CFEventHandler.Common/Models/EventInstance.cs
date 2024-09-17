using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Event instance
    /// </summary>
    public class EventInstance
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Event type
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Client that created event
        /// </summary>
        public string EventClientId { get; set; } = String.Empty;

        /// <summary>
        /// Time created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Parameters
        /// </summary>
        public List<EventParameter> Parameters { get; set; }
    }
}
