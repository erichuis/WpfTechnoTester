using WpfTechnoTester.Models;

namespace WpfTechnoTester.Services
{
    public interface IUserService
    {
        Task LoginAsync(User user);
        Task<bool> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

    }
}
