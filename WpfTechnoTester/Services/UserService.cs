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


        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(string username, SecureString password)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateManyAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
