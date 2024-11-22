using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Models;

namespace WpfTechnoTester.Services
{
    public class UserService : IUserService
    {
        IHttpAppClient _httpAppClient;
        public UserService(IHttpAppClient client)
        {
            _httpAppClient = client;
        }

        public async Task AddUserAsync(User user)
        {
            await _httpAppClient.CreateUser(user);
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
