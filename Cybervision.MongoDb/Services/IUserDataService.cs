using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface IUserDataService : IDataService<UserDto>
    {
        Task<UserDto> Login(UserDto userDto);
        Task<bool> Logout(string username);
        Task<UserDto> GetByName(string username);
    }
}
