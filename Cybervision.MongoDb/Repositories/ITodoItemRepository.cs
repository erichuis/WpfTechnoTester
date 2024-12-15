using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface ITodoItemRepository
    {
        Task<IAsyncEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> GetByIdAsync(Guid id);
        Task<TodoItemDto> GetByTitleAsync(string title);
        Task<TodoItemDto> CreateAsync(TodoItemDto todoItem);
        Task<bool> UpdateAsync(TodoItemDto updatedTodoItem);
        Task<bool> DeleteAsync(Guid id);
    }
}
