using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public class TodoItemDataService : BaseDataService<TodoItemDto, ITodoItemRepository, TodoItemDocument>, ITodoItemDataService
    {
        public TodoItemDataService(ITodoItemRepository repository):base(repository)
        {

        }
        
    }
}
