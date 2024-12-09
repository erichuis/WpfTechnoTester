using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItemDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemDto> UpdateAsync(TodoItemDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemDto> UpdateManyAsync(IEnumerable<TodoItemDto> entities)
        {
            throw new NotImplementedException();
        }

        Task<TodoItemDto> IDataService<TodoItemDto>.CreateAsync(TodoItemDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
