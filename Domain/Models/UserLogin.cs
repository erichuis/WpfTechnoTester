using Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Domain.Models
{
    public class UserLogin
    {
        public UserLogin()
        {
            Username = string.Empty;
        }
        public UserLogin(string username, SecureString password, SecureString passwordVerified)
        {
            ArgumentNullException.ThrowIfNull(username);
            ArgumentNullException.ThrowIfNull(password);

            Username = username;
            Password = password;

        }

        [Required(ErrorMessage = "Username can not be empty")]
        [StringLength(20)]
        public string Username { get; set; }

        [PasswordNotEmptyValidator]
        //[PasswordVerifiedValidator]
        public SecureString? Password { get; set; }

    }
}
