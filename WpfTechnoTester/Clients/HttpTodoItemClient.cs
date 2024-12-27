using Domain.DataTransferObjects;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    class HttpTodoItemClient : IHttpTodoItemClient
    {
        //Todo retrieve this from config
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7116/api/") };
        private DiscoveryDocumentResponse _disco = new DiscoveryDocumentResponse();
        private string _accessToken = string.Empty;
        const string ClientId = "WpfTodo";
        const string ClientSecret = "secret";
        const string Scope = "TodoApi";
        const string GrantType = "client_credentials";

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
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
        public async Task<TodoItemDto> GetByIdAsync(Guid id)
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

        public async Task<TodoItemDto> CreateAsync(TodoItemDto item)
        {
            //string jsonContent = JsonSerializer.Serialize(item);
            //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync($"TodoItem/CreateTodoItem", item).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var newItem = JsonSerializer.Deserialize<TodoItemDto>(json);
            if (newItem == null)
            {
                throw new Exception("No Todoitem created");
            }
            return newItem;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"TodoItem/DeleteTodoItem/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No todo item found");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(TodoItemDto todoItem)
        {
            ArgumentNullException.ThrowIfNull(todoItem);

            // Send the PUT request
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"TodoItem/UpdateTodoItem", todoItem).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No Task found");
            }
            return result;
        }
    }
}
