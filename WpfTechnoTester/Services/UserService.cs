using WpfTechnoTester.Clients;
using WpfTechnoTester.Models;

namespace WpfTechnoTester.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpAppClient _httpAppClient;
        public UserService(IHttpAppClient client)
        {
            _httpAppClient = client;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var response = await _httpAppClient.CreateUser(user);
            if(response == null)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task SignupAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
