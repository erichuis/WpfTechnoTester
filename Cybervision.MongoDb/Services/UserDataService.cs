using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public class UserDataService : BaseDataService<UserDto, IUserRepository, UserDocument>, IUserDataService
    {
        public UserDataService(IUserRepository repository):base(repository)
        {

        }
    }
}
