using Domain.DataTransferObjects;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    class HttpUserClient : HttpBaseClient, IHttpUserClient
    {
      
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
                Address = _disco?.RevocationEndpoint,
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

        public async Task<UserDto> CreateAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("User/CreateUser", user).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            if (result == null)
            {
                throw new Exception("No user created");
            }

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
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

        public async Task<bool> UpdateAsync(UserDto user)
        {
            ArgumentNullException.ThrowIfNull(user);

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

        public async Task<IEnumerable<UserDto>> GetAllAsync()
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

        public async Task<UserDto> GetByIdAsync(Guid id)
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

    }
}
