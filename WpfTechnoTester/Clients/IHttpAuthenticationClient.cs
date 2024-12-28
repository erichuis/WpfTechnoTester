using Domain.DataTransferObjects;
using System.Net.Http;
using System.Security;

namespace WpfTechnoTester.Clients
{
    public interface IHttpAuthenticationClient
    {
        Task<bool> Logout();
        Task<UserDto> Login(string username, SecureString password);
        public string AccessToken { get;}
        public HttpClient Client { get;}
    }
}
