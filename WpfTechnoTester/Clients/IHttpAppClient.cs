using WpfTechnoTester.Models;

namespace WpfTechnoTester.Clients
{
    public interface IHttpAppClient
    {
        Task<IEnumerable<TodoItem>> GetAllTasksAsync();
        Task<TodoItem> GetTodoItemByIdAsync(string id);
        Task<TodoItem> CreateTodoItemAsync(TodoItem item);
        Task<bool> DeleteTodoItemByIdAsync(string id);
        Task<bool> UpdateTodoItemAsync(TodoItem item);
        Task GetToken();
        Task<bool> Logout();
        Task<bool> Login(User user);
        Task<User> CreateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByIdAsync(string id);
    }
}
