using IdentityModel.OidcClient;

namespace WpfTechnoTester.Clients
{
    public class OidcLoginService
    {
        private readonly OidcClient _oidcClient;

        public OidcLoginService()
        {
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:5001",
                ClientId = "WpfTodo",
                RedirectUri = "http://localhost:7890/",
                Scope = "openid profile TodoApi api2 offline_access",
                Browser = new SystemBrowser(7890),
                ClientSecret = "secret"
            };

            _oidcClient = new OidcClient(options);
        }

        public async Task<LoginResult> LoginAsync()
        {
            return await _oidcClient.LoginAsync(new LoginRequest()).ConfigureAwait(false);
        }
    }

}
