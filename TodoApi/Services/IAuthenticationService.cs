using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> Login(UserDto userDto);
        Task<bool> Logout(string username);
    }
}
