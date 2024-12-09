using Domain.DataTransferObjects;
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
        }
        private readonly IUserService _userService;
        public User? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public async Task<bool> Login(string userName, SecureString password)
        {
            try
            {
                CurrentUser = await _userService.Login(userName, password);
                return true;
            }
            catch (Exception)
            {
                //Todo add logging
                return false;
            }
        }

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
                UserName = username
            };

            if(!user.IsValid())
            {
                throw new Exception( user.FailMessages().ToString());
            }

            return await _userService.CreateAsync(user);
        }
    }
}
