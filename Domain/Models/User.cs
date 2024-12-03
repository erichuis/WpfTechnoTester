using System.Net;
using System.Security;
using System.Text.RegularExpressions;

namespace WpfTechnoTester.Models
{
    public class User : BaseModel
    {
        public User() { }
        public User(string userName, string email, SecureString password, SecureString passwordVerified)
        {
            ArgumentNullException.ThrowIfNull(userName);
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(passwordVerified);

            UserName = userName;
            Email = email;
            Password = password;
            PasswordVerified = passwordVerified;

        }
        public string? UserName {  get; set; }
        public string? Email { get; set; }
        public SecureString? Password { get; set; }
        public SecureString? PasswordVerified { get; set; }

        public bool CanSave()
        {
            Reset();
            CheckIsValid(() => !string.IsNullOrEmpty(Email), "Email can not be empty");
            CheckIsValid(() => !string.IsNullOrEmpty(UserName), "Username can not be empty");
            CheckIsValid(() => Password != null && Password.Length != 0, "Password can not be empty");
            CheckIsValid(() => CheckPasswordVerified(Password, PasswordVerified), "Passwords are not the same");
            CheckIsValid(() => CheckPasswordIsStrongEnough(Password), "Password is not strong enough");

            if (IsValid())
            {
                return true;
            }
            else
            {
                return false;
            }
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
