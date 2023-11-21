using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodenameRome.Models
{
    public class MenuItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }
    }
}
