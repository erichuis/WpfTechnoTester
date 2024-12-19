using Domain.Models;
using System.Security;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.State
{
    public class Authenticator : IAuthenticator
    {
        public Authenticator(IUserService userService) 
        {
            _userService = userService;
            ErrorMessage = string.Empty;
           CurrentUser = new User(); //only for dev purposes
        }
        private readonly IUserService _userService;
        public User? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public async Task<bool> Login(string userName, SecureString password)
        {
            try
            {
                CurrentUser = await _userService.Login(userName, password).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public string ErrorMessage { get; private set; }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<User> Register(string email, string username, SecureString password, SecureString passwordConfirmed)
        {
            User user = new()
            {
                Email = email,
                Password = password,
                PasswordVerified = passwordConfirmed,
                Username = username
            };

            //if(!user.IsValid())
            //{
            //    throw new Exception( user.FailMessages().ToString());
            //}

            return await _userService.CreateAsync(user);
        }
    }
}
