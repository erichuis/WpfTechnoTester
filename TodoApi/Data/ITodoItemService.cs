using TodoApi.Models;

namespace TodoApi.Data
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetTodoItems();

        Task<TodoItem> GetTodoItem(string id);

        Task<TodoItem> CreateTodoItem(TodoItem task);

        Task UpdateTodoItem(TodoItem updatedTask);

        Task DeleteTodoItem(string id);
    }
}
