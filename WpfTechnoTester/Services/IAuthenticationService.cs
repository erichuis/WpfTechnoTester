using Domain.Models;
using System.Security;

namespace WpfTechnoTester.Services
{
    public interface IAuthenticationService
    {
        Task<User> Login(string username, SecureString password);
        Task<bool> Logout();
    }
}
