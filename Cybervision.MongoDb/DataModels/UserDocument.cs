using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security;

namespace Cybervision.Dapr.DataModels
{
    public class UserDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        [BsonIgnore]
        public required SecureString Password { get; set; }
        public string? PasswordHashed { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
        public DateTime DateJoined { get; set; }
    }
}
