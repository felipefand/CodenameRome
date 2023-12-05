using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodenameRome.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("phoneNumber")]
        public int PhoneNumber { get; set; }
        [BsonElement("customerFrom")]
        public string[]? CustomerFrom { get; set; }
    }
}
