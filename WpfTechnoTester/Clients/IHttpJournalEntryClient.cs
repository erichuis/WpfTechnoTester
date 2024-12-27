using Domain.DataTransferObjects;

namespace WpfTechnoTester.Clients
{
    public interface IHttpJournalEntryClient
    {
        Task<IEnumerable<JournalEntryDto>> GetAllAsync();
        Task<JournalEntryDto> GetByIdAsync(Guid id);
        Task<IEnumerable<JournalEntryDto>> GetAllByCategoryAsync(string category);
        Task<IEnumerable<JournalEntryDto>> GetAllByDateAsync(DateTime date);
        Task<IEnumerable<JournalEntryDto>> FindAll(string search);
        Task<JournalEntryDto> CreateAsync(JournalEntryDto entry);
        Task<bool> UpdateAsync(JournalEntryDto updatedEntry);
        Task<bool> DeleteAsync(Guid id);
    }
}
