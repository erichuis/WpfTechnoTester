using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;
using WpfTechnoTester.Clients;

namespace WpfTechnoTester.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMapper _mapper;
        private readonly IHttpTodoItemClient _httpAppClient;

        public TodoItemService(IMapper mapper, IHttpTodoItemClient appClient) 
        {
            _mapper = mapper;
            _httpAppClient = appClient;
        }
        public async Task<TodoItem> CreateAsync(TodoItem entity)
        {
            var dto = _mapper.Map<TodoItemDto>(entity);
            var result = await _httpAppClient.CreateAsync(dto).ConfigureAwait(false);
            return _mapper.Map<TodoItem>(result);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _httpAppClient.DeleteAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            var result = await _httpAppClient.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<TodoItem>>( result);
        }

        public async Task<TodoItem> GetAsync(Guid id)
        {
            var result = await _httpAppClient.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<TodoItem>(result);
        }

        public async Task<bool> UpdateAsync(TodoItem entity)
        {
            var dto = _mapper.Map<TodoItemDto>(entity);
            var result = await _httpAppClient.UpdateAsync(dto).ConfigureAwait(false);

            return result;
        }

        public Task<TodoItem> UpdateManyAsync(IEnumerable<TodoItem> entities)
        {
            throw new NotImplementedException();
        }
    }
}
