using Domain.DataTransferObjects;
using Domain.Models;
using System.Security;

namespace WpfTechnoTester.Services
{
    public interface IUserService : IDataService<User>
    {
        Task<User> Login(string username, SecureString password);
        Task<bool> Logout();
    }
}
