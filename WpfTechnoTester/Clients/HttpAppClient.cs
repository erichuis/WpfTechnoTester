using Domain.DataTransferObjects;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Text;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    class HttpAppClient : IHttpAppClient
    {
        //Todo retrieve this from config
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7116/api/") };
        private DiscoveryDocumentResponse _disco = new DiscoveryDocumentResponse();
        private string _accessToken = string.Empty;
        const string ClientId = "WpfTodo";
        const string ClientSecret = "secret";
        const string Scope = "TodoApi";
        const string GrantType = "client_credentials";

        public async Task GetToken()
        {
            var client = new HttpClient();
            _disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

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

        public async Task<UserDto> Login(string username, SecureString password)
        {
            if(string.IsNullOrEmpty(_accessToken))
            {
                await GetToken().ConfigureAwait(false);
            }
            var loginData = new UserDto()
            {
                Username = username,
                UserId = Guid.Empty, //Todo maybe later use for identiication...
                Password = password
            };

            //string json = JsonSerializer.Serialize(loginData);

            //var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("User/Login", loginData).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadFromJsonAsync<UserDto>();
                    Console.WriteLine($"Login successful");
                    if(responseBody != null)
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
            if (string.IsNullOrEmpty(_accessToken))
            {
                //todo logsomething
                return false;
            }
            var response = await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = _disco.RevocationEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Token = _accessToken
            });

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                //Todo log something
                return false;
            }
            _httpClient.SetBearerToken("");
            _accessToken = string.Empty;
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

        public async Task<IEnumerable<TodoItemDto>> GetAllTodoItemsAsync()
        {
            var response = await _httpClient.GetAsync("TodoItem/GetAllTodoItems");
            
            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var todoItems = JsonSerializer.Deserialize<List<TodoItemDto>>(json);
            if (todoItems == null)
            {
                return [];
            }
            return todoItems;
        }

        public async Task<TodoItemDto> GetTodoItemByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"TodoItem/GetTodoItem{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var todoItem = JsonSerializer.Deserialize<TodoItemDto>(json);

            if (todoItem == null)
            {
                throw new Exception("No TodoItem found");
            }
            return todoItem;
        }

        public async Task<TodoItemDto> CreateTodoItemAsync(TodoItemDto item)
        {
            //string jsonContent = JsonSerializer.Serialize(item);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync($"TodoItem/CreateTodoItem", item).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var task = JsonSerializer.Deserialize<TodoItemDto>(json);
            if (task == null)
            {
                throw new Exception("No Todoitem found");
            }
            return task;
        }

        public async Task<bool> DeleteTodoItemByIdAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"TodoItem/DeleteTodoItem{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No todo item found");
            }
            return result;
        }

        public async Task<bool> UpdateTodoItemAsync(TodoItemDto todoItem)
        {
            ArgumentNullException.ThrowIfNull(todoItem);

            //string jsonContent = JsonSerializer.Serialize(taskItem);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the PUT request
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"TodoItem/UpdateTodoItem{todoItem.Id}", todoItem);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No Task found");
            }
            return result;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            //var newUser = new
            //{
            //    username = user.Username,
            //    password = user.Password,
            //    email = user.Email,
            //};

            //string jsonContent = JsonSerializer.Serialize(newUser);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync("User/CreateUser", user).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            if(result == null)
            {
                throw new Exception("No user created");
            }

            //var result = JsonSerializer.Deserialize<string>(json);
            //if (result == null)
            //{
            //    throw new Exception("No UserDto created");
            //}
            return result;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"User/DeleteUser{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception($"No user found for id '{id}'");
            }
            return result;
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            ArgumentNullException.ThrowIfNull(user);

            //string jsonContent = JsonSerializer.Serialize(taskItem);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the PUT request
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"TodoItem/UpdateTodoItem{user.Id}", user);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No user found");
            }
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("TodoItem/GetAllUsers");

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var users = JsonSerializer.Deserialize<List<UserDto>>(json);
            if (users == null)
            {
                return [];
            }
            return users;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"TodoItem/GetUser{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<UserDto>(json);

            if (user == null)
            {
                throw new Exception("No user found");
            }
            return user;
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
