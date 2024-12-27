using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;
        public JournalEntryService(ITodoItemRepository todoItemRepository, IMapper mapper) 
        {
            _repository = todoItemRepository;
            _mapper = mapper;
        }
        public async Task<TodoItemDto> CreateAsync(TodoItemDto entity)
        {
            entity.TodoItemId = Guid.NewGuid();
            var result = await _repository.CreateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id).ConfigureAwait(false);
            return result;
        }

        public async IAsyncEnumerable<TodoItemDto> GetAllAsync()
        {
            var results = _repository.GetAllAsync().ConfigureAwait(false);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async Task<TodoItemDto> GetAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> UpdateAsync(TodoItemDto entity)
        {
            var result = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<TodoItemDto> UpdateManyAsync(IEnumerable<TodoItemDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
