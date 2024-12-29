using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;

namespace Cybervision.Dapr.Repositories
{
    public class UserRepository : BaseRepository<UserDto, UserDocument>, IUserRepository
    {
        public UserRepository(IConfiguration config, IMapper mapper) : base(config, mapper, "Users")
        {
        }
    }
}
