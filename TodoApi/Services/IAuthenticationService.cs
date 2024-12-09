using Domain.DataTransferObjects;
using System.Security;

namespace TodoApi.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string username, SecureString password, SecureString passwordConfirmed, bool isAdmin);
        Task<UserDto> Login(string username, SecureString password);
    }
}
