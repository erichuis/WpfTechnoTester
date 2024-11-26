using TodoApi.Models;

namespace TodoApi.Data
{
    public interface IUserRepository
    {
        Task<IAsyncEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(string id);

        Task<User> GetByNameAsync(string name);

        Task<User> CreateAsync(User user);

        Task<bool> UpdateAsync(User updatedUser);

        Task<bool> DeleteAsync(string id);
    }
}
