using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security;

namespace TodoApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required SecureString Password { get; set; }
        public bool IsActive { get; set; }
        public required string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
    }
}
