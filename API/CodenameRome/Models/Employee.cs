using CodenameRome.Application.Auth;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using BC = BCrypt.Net.BCrypt;

namespace CodenameRome.Models
{
    public enum EmployeeRole
    {
        Admin = 0,
        Owner = 10,
        Manager = 20,
        Employee = 30
    }
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = String.Empty;
        [BsonElement("clientId")]
        public string ClientId { get; set; } = String.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("address")]
        public string? Address { get; set; }
        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }
        [BsonElement("salary")]
        public decimal? Salary { get; set; } = 0;
        [BsonElement("username")]
        public string? Email { get; set; }
        [BsonIgnore]
        public string? Password
        {
            get => passwordHash;
            set
            {
                if (value != null)
                {
                    if (value.Length < 6 || value.Length > 16)
                        throw new Exception("Password too long or too short.");

                    passwordHash = BC.HashPassword(value);
                }
            }
        }

        [BsonElement("passwordHash")]
        private string? passwordHash;

        [BsonElement("role")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EmployeeRole Role { get; set; }

        public bool VerifyPassword(string password)
        {
            var isPasswordCorrect = BC.Verify(password, this.passwordHash!);
            if (isPasswordCorrect)
                return true;

            return false;
        }

        public void ObfuscatePassword()
        {
            if (this.passwordHash != null)
                this.passwordHash = "******";
        }
    }
}
