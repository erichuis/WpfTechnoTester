using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;

namespace Domain.DataModels
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
