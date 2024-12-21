using Domain.DataTransferObjects;
using System.Security;

namespace TodoApi.Services
{
    public interface IUserService : IDataService<UserDto>
    {
        Task<UserDto> Login(UserDto userDto);
        Task<bool> Logout(string username);
        Task<UserDto> GetByName(string username);
    }
}
