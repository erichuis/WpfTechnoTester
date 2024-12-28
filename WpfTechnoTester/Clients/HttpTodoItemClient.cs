using Domain.DataTransferObjects;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    public class HttpTodoItemClient : IHttpTodoItemClient
    {
        private readonly IHttpAuthenticationClient _httpAuthenticationClient;
        private readonly HttpClient _httpClient;
        public HttpTodoItemClient(IHttpAuthenticationClient httpAuthenticationClient)
        {
            _httpAuthenticationClient = httpAuthenticationClient;
            _httpClient = _httpAuthenticationClient.Client;

        }
      
        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("TodoItem/GetAllTodoItems");

            //todo make generic check if responses are ok..

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
