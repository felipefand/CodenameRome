using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodenameRome.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("address")]
        public string Address { get; set; } = String.Empty;
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = String.Empty;
        [BsonElement("ownerName")]
        public string OwnerName { get; set; } = String.Empty;
        [BsonElement("ownerEmail")]
        public string OwnerEmail { get; set; } = String.Empty;
        [BsonElement("ownerId")]
        public string OwnerId { get; set; } = String.Empty;
        [BsonElement("isLive")]
        public bool IsLive { get; set; } = true;
    }
}
