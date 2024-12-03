using Domain.Models;
using System.Net;
using System.Security;
using Domain.Helpers;

namespace TodoApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<UserDto> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(string email, string username, SecureString password, SecureString passwordConfirmed, bool isAdmin)
        {
            if(!new NetworkCredential(string.Empty, password).Password.Equals(
                new NetworkCredential(string.Empty, passwordConfirmed)))
            {
                return Task.FromResult(false); 
            }

            UserDto user = new UserDto { 
                Email = email,
                Username = username,
                DateJoined = DateTime.UtcNow,
                Password = password,
                IsActive = true,
                IsAdmin = isAdmin,
            };
            // Hash the password
            user.PasswordHashed = PasswordHelper.HashPassword(new NetworkCredential(string.Empty, user.Password).Password);

            return Task.FromResult(true);
        }
    }
}
