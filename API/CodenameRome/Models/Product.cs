using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodenameRome.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = String.Empty;
        [BsonElement("clientId")]
        public string ClientId { get; set; } = String.Empty;

        [BsonElement("category")]
        public string Category { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("price")]
        public int Price { get; set; } = 0;

        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;
    }
}
