using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface ITodoItemRepository
    {
        Task<IAsyncEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> GetByIdAsync(string id);
        Task<TodoItemDto> GetByTitleAsync(string title);
        Task<TodoItemDto> CreateAsync(TodoItemDto todoItem);
        Task<bool> UpdateAsync(TodoItemDto updatedTodoItem);
        Task<bool> DeleteAsync(string id);
    }
}
