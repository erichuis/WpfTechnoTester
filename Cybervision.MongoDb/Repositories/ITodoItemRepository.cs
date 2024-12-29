using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Repositories
{
    public interface ITodoItemRepository : IBaseRepository<TodoItemDto, TodoItemDocument>
    {
    }
}
