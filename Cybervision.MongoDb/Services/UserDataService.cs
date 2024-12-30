using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public class UserDataService : BaseDataService<UserDto, UserRepository, UserDocument>, IUserDataService
    {
        public UserDataService(UserRepository repository):base(repository)
        {

        }

        public async Task<UserDto> GetByName(string username)
        {
            var result = await _repository.GetBySearchKey(username).ConfigureAwait(false);
            return result;
        }

        public Task<UserDto> Login(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(string username)
        {
            throw new NotImplementedException();
        }
    }
}
