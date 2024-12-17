using Domain.DataTransferObjects;
using Domain.Models;
using System.Security;

namespace WpfTechnoTester.State
{
    public interface IAuthenticator
    {
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task<User> Register(string email, string userName, SecureString password, SecureString passwordConfirmed);

        Task<bool> Login(string userName, SecureString password);
        void Logout();

        string ErrorMessage {  get; }
    }
}
