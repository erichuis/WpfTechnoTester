using Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace Domain.Models
{
    public class User : BaseModel
    {
        //private const string EmptyPasswordErrorMessage = "Password can not be empty.";

        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
        }
        public User(string username, string email, SecureString password, SecureString passwordVerified)
        {
            ArgumentNullException.ThrowIfNull(username);
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(passwordVerified);

            Username = username;
            Email = email;
            Password = password;
            PasswordVerified = passwordVerified;

        }

        [Required(ErrorMessage = "Username can not be empty")]
        [StringLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email can not be empty")]
        [StringLength(80)]
        public string Email { get; set; }

        [PasswordNotEmptyValidator]
        [StrongPasswordValidator]
        //[PasswordVerifiedValidator]
        public SecureString? Password { get; set; }

        [PasswordNotEmptyValidator]
        [StrongPasswordValidator]
        [PasswordVerifiedValidator]
        public SecureString? PasswordVerified { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
