using Domain.Models;
using System.Security;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.State
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        public Authenticator(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
            ErrorMessage = string.Empty;
        }
        public User? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public async Task<bool> Login(string userName, SecureString password)
        {
            try
            {
                CurrentUser = await _authenticationService.Login(userName, password).ConfigureAwait(false);
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
    }
}
