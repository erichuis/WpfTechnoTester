using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cybervision.Dapr.Services
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IMongoCollection<TodoItemDocument> _todoItems;
        private readonly IMapper _mapper;

        public TodoItemRepository(IConfiguration config, IMapper mapper)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
           
            _todoItems = database.GetCollection<TodoItemDocument>("TodoItems");
            _mapper = mapper;
        }

        public async Task<IAsyncEnumerable<TodoItemDto>> GetAllAsync()
        {
            var list = await _todoItems.Find(task => true).ToListAsync();
            return _mapper.Map<IAsyncEnumerable<TodoItemDto>>(list);
        }

        public async Task<TodoItemDto> GetByIdAsync(Guid id) 
        {
            var todoItem = await _todoItems.Find(task => task.TodoItemId == id).FirstOrDefaultAsync();
            return _mapper.Map<TodoItemDto>(todoItem);
        }

        public async Task<TodoItemDto> GetByTitleAsync(string title) 
        {
            var todoItem = await _todoItems.Find(task => task.Title == title).FirstOrDefaultAsync();
            return _mapper.Map<TodoItemDto>(todoItem);
        }

        public async Task<TodoItemDto> CreateAsync(TodoItemDto todoItem)
        {

            var todoItemDocument = _mapper.Map<TodoItemDocument>(todoItem);
            await _todoItems.InsertOneAsync(todoItemDocument, new InsertOneOptions { BypassDocumentValidation = true });
            //Todo retrieve Id

            return todoItem;
        }

        public async Task<bool> UpdateAsync(TodoItemDto updatedTodoItem)
        {
            var todoItemDocument = _mapper.Map<TodoItemDocument>(updatedTodoItem);
            var result = await _todoItems.ReplaceOneAsync(todoItem => todoItem.TodoItemId == updatedTodoItem.Id, todoItemDocument);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _todoItems.DeleteOneAsync(todoItem => todoItem.TodoItemId == id);
            return result.IsAcknowledged;
        }
    }
}
