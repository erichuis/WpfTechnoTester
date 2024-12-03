using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
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
