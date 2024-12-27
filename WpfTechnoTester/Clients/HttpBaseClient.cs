using IdentityModel.Client;
using System.Net.Http;

namespace WpfTechnoTester.Clients
{
    public class HttpBaseClient
    {
        //Todo retrieve this from config
        protected readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7116/api/") };
        protected DiscoveryDocumentResponse _disco = new DiscoveryDocumentResponse();
        protected string _accessToken = string.Empty;
        protected const string ClientId = "WpfTodo";
        protected const string ClientSecret = "secret";
        const string Scope = "TodoApi";
        const string GrantType = "client_credentials";

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

        public async Task GetToken()
        {
            var client = new HttpClient();
            _disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001").ConfigureAwait(false);

            if (_disco.IsError)
            {
                Console.WriteLine(_disco.Error);
                return;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _disco.TokenEndpoint,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    Scope = Scope,
                    GrantType = GrantType
                });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"{tokenResponse.Error}");
                return;
            }

            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                //context.Response.StatusCode = 401;
                return;
            }

            _httpClient.SetBearerToken(tokenResponse.AccessToken);
            _accessToken = tokenResponse.AccessToken;
        }
    }
}
