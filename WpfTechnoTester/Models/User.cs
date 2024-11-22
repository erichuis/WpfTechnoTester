using System.Security;

namespace WpfTechnoTester.Models
{
    public class User : BaseModel
    {
        public User() { }
        public User(string userName, string email, string emailVerified, SecureString password)
        {
            ArgumentNullException.ThrowIfNull(userName);
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(emailVerified);
            ArgumentNullException.ThrowIfNull(password);

            UserName = userName;
            Email = email;
            EmailVerified = emailVerified;
            Password = password;

        }
        public string? UserName {  get; set; }
        public string? Email { get; set; }
        public string? EmailVerified { get; set; }
        public SecureString? Password { get; set; }

        public bool CanSave()
        {
            CheckIsValid(() => !string.IsNullOrEmpty(Email), "Email can not be empty");
            CheckIsValid(() => !string.IsNullOrEmpty(EmailVerified), "EmailVerified can not be empty");
            CheckIsValid(() => !string.IsNullOrEmpty(UserName), "Username can not be empty");
            CheckIsValid(() => Password?.Length != 0, "Password can not be empty");
            CheckIsValid(() => Email != null && Email.Equals(EmailVerified), "Email verification is not correct");

            if (IsValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Save()
        {
            if (CanSave())
            {
                return true;
            }
            return false;
        }
    }
}
