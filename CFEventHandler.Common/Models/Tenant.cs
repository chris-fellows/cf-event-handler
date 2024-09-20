using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CFEventHandler.Models
{
    public class Tenant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string DatabaseName { get; set; } = String.Empty;

        public string ConnectionString { get; set; } = String.Empty;
    }
}
