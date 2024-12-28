using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Profiles
{
    public class TodoItemProfile : Profile
    {   
        public TodoItemProfile()
        {
            CreateMap<TodoItemDocument, TodoItemDto>();
            CreateMap<TodoItemDto, TodoItemDocument>();
        }
    }
}
