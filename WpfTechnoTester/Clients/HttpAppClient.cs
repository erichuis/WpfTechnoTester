﻿using Domain.DataTransferObjects;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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

        public async Task<bool> Login(UserDto login)
        {
            var loginData = new
            {
                username = login.Username,
                password = login.Password
            };

            string json = JsonSerializer.Serialize(loginData);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("Users/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Login successful: {responseBody}");
                    return true; // Contains token or user details
                }
                else
                {
                    Console.WriteLine($"Login failed: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
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

        public async Task<IEnumerable<TodoItemDto>> GetAllTodoItemsAsync()
        {
            var response = await _httpClient.GetAsync("TodoItem/GetAllTodoItems");
            
            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var tasks = JsonSerializer.Deserialize<List<TodoItemDto>>(json);
            if (tasks == null)
            {
                return [];
            }
            return tasks;
        }

        public async Task<TodoItemDto> GetTodoItemByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"TodoItem/GetTodoItem{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var task = JsonSerializer.Deserialize<TodoItemDto>(json);

            if (task == null)
            {
                throw new Exception("No TodoItem found");
            }
            return task;
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
                throw new Exception("No Task found");
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
            var newUser = new
            {
                username = user.Username,
                password = user.Password,
                email = user.Email,
            };

            //string jsonContent = JsonSerializer.Serialize(newUser);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync($"User/CreateUser", newUser).ConfigureAwait(false);

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

        public Task<bool> DeleteUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByIdAsync(Guid id)
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
