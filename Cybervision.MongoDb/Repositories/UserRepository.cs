using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Cybervision.Dapr.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDocument> _users;
        private readonly IMapper _mapper;

        public UserRepository(IConfiguration config, IMapper mapper)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
            _users = database.GetCollection<UserDocument>("Users");
            _mapper = mapper;
        }

        public async Task<IAsyncEnumerable<UserDto>> GetAllAsync()
        {
            var list = (IAsyncEnumerable<UserDto>)await _users.Find(UserDocument => true).ToListAsync();
            return _mapper.Map<IAsyncEnumerable<UserDto>>(list);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _users.Find(user => user.UserId == id).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByNameAsync(string name)
        {
            var user = await _users.Find(user => user.Username == name).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserDto user)
        {
            var userDocument = _mapper.Map<UserDocument>(user);
            await _users.InsertOneAsync(userDocument, new InsertOneOptions { BypassDocumentValidation = true });
            //Todo retrieve Id

            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _users.DeleteOneAsync(user => user.UserId == id);
            return result.IsAcknowledged;
        }

        public async Task<bool> UpdateAsync(UserDto updatedUser)
        {
            var userDocument = _mapper.Map<UserDocument>(updatedUser);
            var result = await _users.ReplaceOneAsync(user => user.UserId == updatedUser.UserId, userDocument);
            return result.IsAcknowledged;
        }
    }
}
