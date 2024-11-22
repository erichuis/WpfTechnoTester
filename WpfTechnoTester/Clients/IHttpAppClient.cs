using WpfTechnoTester.Models;

namespace WpfTechnoTester.Clients
{
    public interface IHttpAppClient
    {
        Task<IEnumerable<TodoItem>> GetAllTasksAsync();
        Task<TodoItem> GetTaskByIdAsync(string id);
        Task<TodoItem> CreateTaskAsync(TodoItem item);
        Task<bool> DeleteTaskByIdAsync(string id);
        Task<bool> UpdateTaskAsync(TodoItem item);
        Task GetToken();
        Task<bool> Logout();
        Task<bool> Login();
        Task<User> CreateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByIdAsync(string id);
    }
}
