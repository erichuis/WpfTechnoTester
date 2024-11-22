using WpfTechnoTester.Models;

namespace WpfTechnoTester.Services
{
    public interface IUserService
    {
        Task SignupAsync(User user);
        Task LoginAsync(User user);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

    }
}
