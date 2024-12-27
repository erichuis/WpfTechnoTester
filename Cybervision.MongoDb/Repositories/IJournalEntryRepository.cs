using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public interface IJournalEntryRepository
    {
        IAsyncEnumerable<JournalEntryDto> GetAllAsync();
        Task<JournalEntryDto> GetByIdAsync(Guid id);
        IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category);
        IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date);
        IAsyncEnumerable<JournalEntryDto> FindAll(string search);
        Task<JournalEntryDto> CreateAsync(JournalEntryDto entry);
        Task<bool> UpdateAsync(JournalEntryDto updatedEntry);
        Task<bool> DeleteAsync(Guid id);
    }
}
