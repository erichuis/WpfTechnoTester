
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDto, UserDocument>
    {
    }
}
