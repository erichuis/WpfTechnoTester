using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class TodoItemService : BaseDataService<TodoItemDto, TodoItemDataService>, ITodoItemService
    {
        public TodoItemService(TodoItemDataService service, IMapper mapper):base(service, mapper)
        {
        }
    }
}
