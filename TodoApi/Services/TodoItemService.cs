using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class TodoItemService : BaseDataService<TodoItemDto, ITodoItemDataService>, ITodoItemService
    {
        public TodoItemService(ITodoItemDataService service, IMapper mapper):base(service, mapper)
        {
        }
    }
}
