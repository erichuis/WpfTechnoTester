using TodoApi.Models;

namespace TodoApi.Data
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(string id);
        Task<TodoItem> GetByTitleAsync(string title);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<bool> UpdateAsync(TodoItem updatedTodoItem);
        Task<bool> DeleteAsync(string id);
    }
}
