using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;
        public TodoItemService(ITodoItemRepository todoItemRepository, IMapper mapper) 
        {
            _repository = todoItemRepository;
            _mapper = mapper;
        }
        public async Task<TodoItemDto> CreateAsync(TodoItemDto entity)
        {
            var result = await _repository.CreateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TodoItemDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoItemDto> UpdateAsync(TodoItemDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoItemDto> UpdateManyAsync(IEnumerable<TodoItemDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
