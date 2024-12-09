using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;
using WpfTechnoTester.Clients;

namespace TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMapper _mapper;
        private readonly IHttpAppClient _httpAppClient;

        public TodoItemService(IMapper mapper, IHttpAppClient appClient) 
        {
            _mapper = mapper;
            _httpAppClient = appClient;
        }
        public async Task<TodoItem> CreateAsync(TodoItem entity)
        {
            var dto = _mapper.Map<TodoItemDto>(entity);
            var result = await _httpAppClient.CreateTodoItemAsync(dto).ConfigureAwait(false);
            return _mapper.Map<TodoItem>(result);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> UpdateManyAsync(IEnumerable<TodoItem> entities)
        {
            throw new NotImplementedException();
        }
    }
}
