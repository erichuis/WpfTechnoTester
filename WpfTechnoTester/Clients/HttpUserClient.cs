using Domain.DataTransferObjects;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    class HttpUserClient : IHttpUserClient
    {
        private readonly IHttpAuthenticationClient _httpAuthenticationClient;
        private readonly HttpClient _httpClient;
        public HttpUserClient(IHttpAuthenticationClient httpAuthenticationClient)
        {
            _httpAuthenticationClient = httpAuthenticationClient;
            _httpClient = _httpAuthenticationClient.Client;
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
