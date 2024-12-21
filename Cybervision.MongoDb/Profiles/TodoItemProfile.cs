using AutoMapper;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.DataModels
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemDocument, TodoItemDto>();
            CreateMap<TodoItemDto, TodoItemDocument>();
            //CreateMap<List<TodoItemDocument>, IAsyncEnumerable<TodoItemDto>>();
            //CreateMap<IAsyncEnumerable<TodoItemDto>, List<TodoItemDocument>>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
