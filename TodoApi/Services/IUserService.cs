using Domain.DataTransferObjects;
using System.Security;

namespace TodoApi.Services
{
    public interface IUserService : IDataService<UserDto>
    {
        Task<UserDto> Login(UserDto userDto);
        Task<UserDto> Logout(string username);
        Task<UserDto> GetByName(string username);
    }
}
