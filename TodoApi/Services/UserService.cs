using Domain.DataTransferObjects;
using System.Net;
using System.Security;
using Domain.Helpers;
using Cybervision.Dapr.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;

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

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserDto>>(result);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<UserDto> GetByName(string username)
        {
            return await _userRepository.GetByNameAsync(username).ConfigureAwait(false);
        }

        public async Task<UserDto> Login(UserDto userDto)
        {
            
            var foundUser = await _userRepository.GetByNameAsync(userDto.Username).ConfigureAwait(false);

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

        public async Task<UserDto> Logout(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> UpdateAsync(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateManyAsync(IEnumerable<UserDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
