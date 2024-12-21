

using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface IUserRepository
    {
        IAsyncEnumerable<UserDto> GetAllAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> GetByNameAsync(string name);

        Task<UserDto> CreateAsync(UserDto user);

        Task<bool> UpdateAsync(UserDto updatedUser);

        Task<bool> DeleteAsync(Guid id);
    }
}
