using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodenameRome.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("login")]
        public string Login { get; set; }
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }
        [BsonElement("salary")]
        public string Salary { get; set; }
        [BsonElement("role")]
        public string Role { get; set; }
    }
}
