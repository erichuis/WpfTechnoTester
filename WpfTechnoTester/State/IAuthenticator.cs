using Domain.DataTransferObjects;
using Domain.Models;
using System.Security;

namespace WpfTechnoTester.State
{
    public interface IAuthenticator
    {
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task<bool> Login(string userName, SecureString password);
        void Logout();

        string ErrorMessage {  get; }
    }
}
