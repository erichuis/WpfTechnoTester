using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public class JournalEntryDataService : BaseDataService<JournalEntryDto, IJournalEntryRepository, JournalEntryDocument>, IJournalEntryDataService
    {
        public JournalEntryDataService(IJournalEntryRepository repository):base(repository)
        {

        }

        public IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search)
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
    }
}
