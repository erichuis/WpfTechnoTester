using AutoMapper;
using Domain.Models;
using System.Security;
using WpfTechnoTester.Clients;

namespace WpfTechnoTester.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpAuthenticationClient _httpUserClient;
        private readonly IMapper _mapper;
        public AuthenticationService(IHttpAuthenticationClient client, IMapper mapper)
        {
            _httpUserClient = client;
            _mapper = mapper;   
        }

        public async Task<User> Login(string username, SecureString password)
        {
            var result = await _httpUserClient.Login(username, password).ConfigureAwait(false);

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
            return await _httpUserClient.Logout();
        }
    }
}
