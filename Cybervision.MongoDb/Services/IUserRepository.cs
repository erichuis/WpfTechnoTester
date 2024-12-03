

using Domain.Models;

namespace Cybervision.Dapr.Services
{
    public interface IUserRepository
    {
        Task<IAsyncEnumerable<UserDto>> GetAllAsync();

        Task<UserDto> GetByIdAsync(string id);

        Task<UserDto> GetByNameAsync(string name);

        Task<UserDto> CreateAsync(UserDto user);

        Task<bool> UpdateAsync(UserDto updatedUser);

        Task<bool> DeleteAsync(string id);
    }
}
