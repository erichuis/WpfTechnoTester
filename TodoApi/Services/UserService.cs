using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;
using Domain.Helpers;

namespace TodoApi.Services
{
    public class UserService : BaseDataService<UserDto, UserDataService>, IUserService
    {
        private readonly IUserDataService _dataService;
        private readonly IMapper _mapper;
        public UserService(UserDataService dataService, IMapper mapper) :base(dataService, mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
    
        public async Task<UserDto> GetByName(string username)
        {
            return await _dataService.GetByName(username).ConfigureAwait(false);
        }

        public async Task<UserDto> Login(UserDto userDto)
        {
            var foundUser = await _dataService.GetByName(userDto.Username).ConfigureAwait(false);

            if (foundUser == null || foundUser.PasswordHashed == null)
            {
                //return BadRequest("No valid user found");
                throw new Exception("No valid user found");
            }

            if (!PasswordHelper.VerifyPassword(userDto?.PasswordHashed ?? string.Empty, foundUser.PasswordHashed))
            {
                //return Unauthorized("Invalid username or password.");
                throw new UnauthorizedAccessException("Invalid username or password");
            }
            return foundUser;
        }

        public async Task<bool> Logout(string username)
        {
            //var result = await _userRepository.
            return true;
        }

        public Task<UserDto> UpdateManyAsync(IEnumerable<UserDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
