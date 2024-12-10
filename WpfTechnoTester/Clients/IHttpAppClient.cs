using Domain.DataTransferObjects;
using System.Security;

namespace WpfTechnoTester.Clients
{
    public interface IHttpAppClient
    {
        Task<IEnumerable<TodoItemDto>> GetAllTodoItemsAsync();
        Task<TodoItemDto> GetTodoItemByIdAsync(Guid id);
        Task<TodoItemDto> CreateTodoItemAsync(TodoItemDto item);
        Task<bool> DeleteTodoItemByIdAsync(Guid id);
        Task<bool> UpdateTodoItemAsync(TodoItemDto item);
        Task GetToken();
        Task<bool> Logout();
        Task<UserDto> Login(string username, SecureString password);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
    }
}
