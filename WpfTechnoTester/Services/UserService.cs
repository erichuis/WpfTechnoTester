using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;
using System.Security;
using WpfTechnoTester.Clients;

namespace WpfTechnoTester.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpAppClient _httpAppClient;
        private readonly IMapper _mapper;
        public UserService(IHttpAppClient client, IMapper mapper)
        {
            _httpAppClient = client;
            _mapper = mapper;   
        }

        public async Task<User> CreateAsync(User entity)
        {
            var dto = _mapper.Map<UserDto>(entity);
            var response = await _httpAppClient.CreateUserAsync(dto);
            var user = _mapper.Map<User>(response);
            return user;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _httpAppClient.DeleteUserAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _httpAppClient.GetAllUsersAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<User>>(result);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result = await _httpAppClient.GetUserByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<User>(result);
        }

        public async Task<User> Login(string username, SecureString password)
        {
            var result = await _httpAppClient.Login(username, password).ConfigureAwait(false);

            if (result != null)
            {
                return _mapper.Map<User>(result);
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        public async Task<bool> Logout()
        {
            return await _httpAppClient.Logout();
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var dto = _mapper.Map<UserDto>(entity); 
            var result = await _httpAppClient.UpdateUserAsync(dto).ConfigureAwait(false);
            return _mapper.Map<User>(result);
        }

        public Task<User> UpdateManyAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
