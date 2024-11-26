using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IMongoCollection<TodoItem> _todoItems;

        public TodoItemRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
            _todoItems = database.GetCollection<TodoItem>("TodoItems");
        }

        public async Task<List<TodoItem>> GetAllAsync() => await _todoItems.Find(task => true).ToListAsync();

        public async Task<TodoItem> GetByIdAsync(string id) => await _todoItems.Find(task => task.Id == id).FirstOrDefaultAsync();

        public async Task<TodoItem> GetByTitleAsync(string title) => await _todoItems.Find(task => task.Title == title).FirstOrDefaultAsync();

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            await _todoItems.InsertOneAsync(todoItem, new InsertOneOptions { BypassDocumentValidation=true});
            return todoItem;
        }

        public async Task<bool> UpdateAsync(TodoItem updatedTodoItem)
        {
            var result = await _todoItems.ReplaceOneAsync(task => task.Id == updatedTodoItem.Id, updatedTodoItem);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _todoItems.DeleteOneAsync(todoItem => todoItem.Id == id);
            return result.IsAcknowledged;
        } 
    }
}
