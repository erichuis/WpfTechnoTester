using AutoMapper;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.DataModels
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDocument, UserDto>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
