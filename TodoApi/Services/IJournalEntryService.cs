using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public interface IJournalEntryService : IDataService<JournalEntryDto>
    {
        IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category);
        IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date);
        IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search);
    }
}
