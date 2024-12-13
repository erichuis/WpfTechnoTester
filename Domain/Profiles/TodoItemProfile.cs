using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;

namespace Domain.DataModels
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
