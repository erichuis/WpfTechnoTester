using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;

namespace Cybervision.Dapr.Repositories
{
    public class JournalEntryRepository : BaseRepository<TodoItemDto, TodoItemDocument>, IJournalEntryRepository
    {
        public JournalEntryRepository(IConfiguration config, IMapper mapper) : base(config, mapper, "JournalEntries")
        {
        }

        public Task<JournalEntryDto> CreateAsync(JournalEntryDto itemToCreate)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<JournalEntryDto> FindAll(string search)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(JournalEntryDto itemToUpdate)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<JournalEntryDto> IBaseRepository<JournalEntryDto, JournalEntryDocument>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<JournalEntryDto> IBaseRepository<JournalEntryDto, JournalEntryDocument>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<JournalEntryDto> IBaseRepository<JournalEntryDto, JournalEntryDocument>.GetBySearchKey(string searchKey)
        {
            throw new NotImplementedException();
        }
    }
}
