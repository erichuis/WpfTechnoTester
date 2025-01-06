using Domain.DataTransferObjects;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Windows;

namespace WpfTechnoTester.Clients
{
    public class HttpAuthenticationClient : IHttpAuthenticationClient
    {
        //protected string _accessToken = string.Empty;
        protected const string ClientId = "WpfTodo";
        protected const string ClientSecret = "secret";
        const string Scope = "TodoApi";
        const string GrantType = "client_credentials";


        public string AccessToken { get; private set; }

        public HttpClient Client { get; private set; }
        private OidcLoginService _oidcLoginService;
        private DiscoveryDocumentResponse _disco = new();

        public HttpAuthenticationClient(OidcLoginService oidcLoginService)
        {
            AccessToken = string.Empty;
            Client = new() { BaseAddress = new Uri("https://localhost:7116/api/") };
            _oidcLoginService = oidcLoginService;
        }


        public async Task<UserDto> Login(string username, SecureString password)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                var result = await _oidcLoginService.LoginAsync();
                if (result.IsError)
                {
                    MessageBox.Show($"Login failed: {result.Error}");
                    AccessToken =  string.Empty;
                  
                }
                AccessToken = result.AccessToken;
                Client.SetBearerToken(AccessToken);
            }
            //Todo this is being handled by identity server...should be removed
            var loginData = new UserDto()
            {
                Username = username,
                UserId = Guid.Empty, //Todo maybe later use for identiication...
                Password = password
            };

            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync("Authentication/Login", loginData).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadFromJsonAsync<UserDto>();
                    Console.WriteLine($"Login successful");
                    if (responseBody != null)
                    {
                        return responseBody; // Contains token or user details
                    }
                    throw new Exception("Authenticated user could not be returned");
                }
                else
                {
                    Console.WriteLine($"Login failed: {response.StatusCode}");
                    throw new Exception("Login failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> Logout()
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                //todo logsomething
                return false;
            }
            var response = await Client.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = _disco?.RevocationEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Token = AccessToken
            });

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                //Todo log something
                return false;
            }
            //_httpClient.SetBearerToken("");
            AccessToken = string.Empty;
            return true;
        }

        public async Task<string> GetToken()
        {
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:5001",
                ClientId = ClientId,
                RedirectUri = "http://localhost:7890/",
                ClientSecret = ClientSecret,
                Scope = Scope,
                Browser = new SystemBrowser(7890)
            };
            var oidcClient = new OidcClient(options);

            var result = await oidcClient.LoginAsync(new LoginRequest());

            if (result.IsError)
            {
                MessageBox.Show($"Login failed: {result.Error}");
                return string.Empty;
            }

            return result.AccessToken;
            //MessageBox.Show("Login successful!");

            //var client = new HttpClient();

            //_disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001").ConfigureAwait(false);

            //if (_disco.IsError)
            //{
            //    Console.WriteLine(_disco.Error);
            //    return string.Empty;
            //}
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            //    new ClientCredentialsTokenRequest
            //    {
            //        Address = _disco.TokenEndpoint,
            //        ClientId = ClientId,
            //        ClientSecret = ClientSecret,
            //        Scope = Scope,
            //        GrantType = GrantType
            //    });

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine($"{tokenResponse.Error}");
            //    return string.Empty;
            //}

            //if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            //{
            //    //context.Response.StatusCode = 401;
            //    return string.Empty;
            //}

            //return tokenResponse.AccessToken;
        }

        private async void CheckError(string token)
        {
            var client = new System.Net.Http.HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/connect/introspect");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "token", token },
                    { "client_id", "WpfTodo" },
                    { "client_secret", "secret" }
                });

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
