using Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public class User : BaseModel
    {
        //private const string EmptyPasswordErrorMessage = "Password can not be empty.";

        public User() 
        {
             Username = string.Empty;
            //Email = string.Empty;
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
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can not be empty.")]
        public SecureString? Password { get; set; }

        [Required(ErrorMessage = "Verification password can not be empty.")]
        public SecureString? PasswordVerified { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }


        //public bool CanLogin()
        //{
        //    Reset();
           
        //    //CheckIsValid(() => !string.IsNullOrEmpty(UserName), "Username can not be empty");
        //    //CheckIsValid(() => Password != null && Password.Length != 0, "Password can not be empty");

        //    if (IsValid())
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        public bool CanSave()
        {
            //Reset();
            //CheckIsValid(() => !string.IsNullOrEmpty(Email), "Email can not be empty");
            //CheckIsValid(() => !string.IsNullOrEmpty(Username), "Username can not be empty");
            //CheckIsValid(() => Password != null && Password.Length != 0, "Password can not be empty");
            //CheckIsValid(() => CheckPasswordVerified(Password, PasswordVerified), "Passwords are not the same");
            //CheckIsValid(() => CheckPasswordIsStrongEnough(Password), "Password is not strong enough");

            //if (IsValid())
            //{
            //    return true;
            //}

            return false;
        }

        private static bool CheckPasswordIsStrongEnough(SecureString? password)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            return validateGuidRegex.IsMatch(new NetworkCredential(string.Empty, password).Password);
        }

        private static bool CheckPasswordVerified(SecureString? password, SecureString? passwordVerified)
        {
            return new NetworkCredential(string.Empty, password).Password.Equals(
                new NetworkCredential(string.Empty, passwordVerified).Password);
        }
    }
}
