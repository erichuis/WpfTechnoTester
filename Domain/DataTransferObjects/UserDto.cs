using System.Security;

namespace Domain.DataTransferObjects
{
    public class UserDto
    {
        public string? Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required SecureString Password { get; set; }
        public string? PasswordHashed { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
        public DateTime DateJoined { get; set; }
    }
}
