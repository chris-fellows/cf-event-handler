using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Net;

namespace CFEventHandler.Models
{
    public class APIKeyInstance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Key { get; set; } = String.Empty;

        public List<string> Roles { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}
