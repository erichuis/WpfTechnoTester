using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMongoCollection<TodoItem> _todoItems;

        public TodoItemService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
            _todoItems = database.GetCollection<TodoItem>("TodoItems");
        }

        public async Task<List<TodoItem>> GetTodoItems() => await _todoItems.Find(task => true).ToListAsync();

        public async Task<TodoItem> GetTodoItem(string id) => await _todoItems.Find(task => task.Id == id).FirstOrDefaultAsync();

        public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            await _todoItems.InsertOneAsync(todoItem);
            return todoItem;
        }

        public async Task UpdateTodoItem(TodoItem updatedTodoItem) =>
            await _todoItems.ReplaceOneAsync(task => task.Id == updatedTodoItem.Id, updatedTodoItem);

        public async Task DeleteTodoItem(string id) => await _todoItems.DeleteOneAsync(todoItem => todoItem.Id == id);
    }
}
