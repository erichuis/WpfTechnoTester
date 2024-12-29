using AutoMapper;
using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;

namespace Cybervision.Dapr.Services
{
    public class TodoItemRepository : BaseRepository<TodoItemDto, TodoItemDocument>, ITodoItemRepository
    {

        public TodoItemRepository(IConfiguration config, IMapper mapper) : base(config, mapper, "TodoItems") 
        {
        }
    }
}
