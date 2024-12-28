using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;
using WpfTechnoTester.Clients;

namespace WpfTechnoTester.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpUserClient _httpUserClient;
        private readonly IMapper _mapper;
        public UserService(IHttpUserClient client, IMapper mapper)
        {
            _httpUserClient = client;
            _mapper = mapper;   
        }

        public async Task<User> CreateAsync(User entity)
        {
            var dto = _mapper.Map<UserDto>(entity);
            var response = await _httpUserClient.CreateAsync(dto);
            var user = _mapper.Map<User>(response);
            return user;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _httpUserClient.DeleteAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _httpUserClient.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<User>>(result);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result = await _httpUserClient.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<User>(result);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var dto = _mapper.Map<UserDto>(entity); 
            var result = await _httpUserClient.UpdateAsync(dto).ConfigureAwait(false);
            return result;
        }

        public Task<User> UpdateManyAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
