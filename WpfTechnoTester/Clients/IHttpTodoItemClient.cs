using Domain.DataTransferObjects;

namespace WpfTechnoTester.Clients
{
    public interface IHttpTodoItemClient
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> GetByIdAsync(Guid id);
        Task<TodoItemDto> CreateAsync(TodoItemDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(TodoItemDto item);
    }
}
