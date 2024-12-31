using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;
using Domain.Helpers;

namespace TodoApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserDataService _dataService;
        public AuthenticationService(IUserDataService dataService) 
        {
            _dataService = dataService;
        }
        public async Task<UserDto> Login(UserDto userDto)
        {
            var foundUser = await _dataService.GetBySearchKeyAsync(userDto.Username).ConfigureAwait(false);

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
    }
}
