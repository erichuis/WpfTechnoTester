using System.Security;
using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public class UserDto : ISearchable
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userid")]
        public required Guid UserId { get; set; }
        [JsonPropertyName("username")]
        public required string Username { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public required SecureString Password { get; set; }
        [JsonPropertyName("passwordhashed")]
        public string? PasswordHashed { get; set; }
        [JsonPropertyName("isactive")]
        public bool IsActive { get; set; } = true;
        [JsonPropertyName("isadmin")]
        public bool IsAdmin { get; set; } = false;
        [JsonPropertyName("datejoined")]
        public DateTime DateJoined { get; set; }
        [JsonIgnore]
        public string SearchKey => Username;
        [JsonIgnore]
        public Guid SearchIdKey => UserId;
    }
}
