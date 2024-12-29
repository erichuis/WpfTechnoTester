using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Repositories
{
    public interface IJournalEntryRepository : IBaseRepository<JournalEntryDto, JournalEntryDocument>
    {
        IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category);
        IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date);
        IAsyncEnumerable<JournalEntryDto> FindAll(string search);
    }
}
