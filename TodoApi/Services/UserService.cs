using AutoMapper;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;
using Domain.Helpers;
using System.Net;

namespace TodoApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Task<UserDto> CreateAsync(UserDto user)
        {
            user.DateJoined = DateTime.Now;
            user.IsActive = true;
            user.UserId = Guid.NewGuid();
          
            // Hash the password
            user.PasswordHashed = PasswordHelper.HashPassword(new NetworkCredential(string.Empty, user.Password).Password);
            
            var result = _userRepository.CreateAsync(user);

            //check if id exists
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<UserDto> GetAllAsync()
        {
            var results =  _userRepository.GetAllAsync();
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<UserDto> GetByName(string username)
        {
            return await _userRepository.GetBySearchKey(username).ConfigureAwait(false);
        }

        public async Task<UserDto> Login(UserDto userDto)
        {
            var foundUser = await _userRepository.GetBySearchKey(userDto.Username).ConfigureAwait(false);

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

        public async Task<bool> UpdateAsync(UserDto entity)
        {
            var result = await _userRepository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public Task<UserDto> UpdateManyAsync(IEnumerable<UserDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
