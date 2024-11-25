using IdentityModel.Client;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WpfTechnoTester.Models;

namespace WpfTechnoTester.Clients
{
    class HttpAppClient : IHttpAppClient
    {
        //Todo retrieve this from config
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7116/api/") };
        private DiscoveryDocumentResponse _disco = new DiscoveryDocumentResponse();
        private TokenResponse _tokenResponse;
        const string ClientId = "WpfTodo";
        const string ClientSecret = "secret";
        const string Scope = "TodoApi";
        const string GrantType = "client_credentials";

        public HttpAppClient()
        {

        }

        public async Task GetToken()
        {
            var client = new System.Net.Http.HttpClient();
            _disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (_disco.IsError)
            {
                Console.WriteLine(_disco.Error);
                return;
            }
            _tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _disco.TokenEndpoint,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    Scope = Scope,
                    GrantType = GrantType
                });

            if (_tokenResponse.IsError)
            {
                Console.WriteLine($"{_tokenResponse.Error}");
                return;
            }

            if (string.IsNullOrEmpty(_tokenResponse.AccessToken))
            {
                //context.Response.StatusCode = 401;
                return;
            }

            _httpClient.SetBearerToken(_tokenResponse.AccessToken);
        }

        public async Task<bool> Logout()
        {
            if (_tokenResponse == null || string.IsNullOrEmpty(_tokenResponse.AccessToken))
            {
                //todo logsomething
                return false;
            }
            var response = await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = _disco.RevocationEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Token = _tokenResponse.AccessToken
            });

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                //Todo log something
                return false;
            }
            _httpClient.SetBearerToken("");
            return true;
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

        public async Task<IEnumerable<TodoItem>> GetAllTasksAsync()
        {
            var response = await _httpClient.GetAsync("Tasks/GetAllTasks");
            if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                return Enumerable.Empty<TodoItem>();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var tasks = JsonSerializer.Deserialize<List<TodoItem>>(json);
            if (tasks == null)
            {
                return [];
            }
            return tasks;
        }

        public async Task<TodoItem> GetTaskByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"tasks/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var task = JsonSerializer.Deserialize<TodoItem>(json);

            if (task == null)
            {
                throw new Exception("No Task found");
            }
            return task;
        }

        public async Task<TodoItem> CreateTaskAsync(TodoItem item)
        {
            string jsonContent = JsonSerializer.Serialize(item);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"tasks", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var task = JsonSerializer.Deserialize<TodoItem>(json);
            if (task == null)
            {
                throw new Exception("No Task found");
            }
            return task;
        }

        public async Task<bool> DeleteTaskByIdAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"tasks/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No Task found");
            }
            return result;
        }

        public async Task<bool> UpdateTaskAsync(TodoItem taskItem)
        {
            ArgumentNullException.ThrowIfNull(taskItem);

            string jsonContent = JsonSerializer.Serialize(taskItem);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the PUT request
            HttpResponseMessage response = await _httpClient.PutAsync($"tasks/UpdateTask{taskItem.Id}", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No Task found");
            }
            return result;
        }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateUser(User newUser)
        {
            string jsonContent = JsonSerializer.Serialize(newUser);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"users", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<User>(json);
            if (user == null)
            {
                throw new Exception("No user found");
            }
            return user;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        //    public static async Task<string> GetAccessTokenAsync()
        //    {
        //        using var client = new HttpClient();

        //        Prepare the token request content
        //       var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001");
        //        request.Content = new FormUrlEncodedContent(new[]
        //        {
        //            new KeyValuePair<string, string>("grant_type", "client_credentials"),
        //            new KeyValuePair<string, string>("client_id", "WpfTodo"),
        //            new KeyValuePair<string, string>("client_secret", "secret"),
        //            new KeyValuePair<string, string>("scope", "TodoApi")
        //        });

        //        Send the request and handle the response
        //       var response = await client.SendAsync(request).ConfigureAwait(false);
        //        response.EnsureSuccessStatusCode();

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(responseBody);

        //        return tokenResponse.AccessToken;
        //    }

        //}
        //public class TokenResponse
        //{
        //    public string AccessToken { get; set; }
        //    public int ExpiresIn { get; set; }
        //}
    }
}
