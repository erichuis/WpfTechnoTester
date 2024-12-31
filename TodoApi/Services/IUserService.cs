using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public interface IUserService : IDataService<UserDto>
    {
    }
}
