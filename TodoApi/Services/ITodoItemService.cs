using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public interface ITodoItemService : IDataService<TodoItemDto>
    {
    }
}
