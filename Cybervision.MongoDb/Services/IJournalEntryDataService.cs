using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface IJournalEntryDataService : IDataService<JournalEntryDto>
    {
        IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category);
        IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date);
        IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search);
    }
}
