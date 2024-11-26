using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IAsyncEnumerable<User>> GetAllAsync()
        {
            return (IAsyncEnumerable<User>) await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _users.Find(user => user.Username == name).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user, new InsertOneOptions { BypassDocumentValidation = true});
            return user;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _users.DeleteOneAsync(todoItem => todoItem.Id == id);
            return result.IsAcknowledged;
        }

        public async Task<bool> UpdateAsync(User updatedUser)
        {
            var result = await _users.ReplaceOneAsync(user => user.Id == updatedUser.Id, updatedUser);
            return result.IsAcknowledged;
        }
    }
}
