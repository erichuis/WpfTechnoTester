using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class JournalEntryService : BaseDataService<JournalEntryDto, IJournalEntryDataService>, IJournalEntryService
    {
        private readonly IJournalEntryDataService _dataService;
        public JournalEntryService(IJournalEntryDataService dataService, IMapper mapper) :base(dataService, mapper)
        {
            _dataService = dataService;
        }

        public async IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search)
        {
            var results = _dataService.FindAllAsync(search).ConfigureAwait(false);

            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category)
        {
            var results = _dataService.GetAllByCategoryAsync(category).ConfigureAwait(false);

            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date)
        {
            var results = _dataService.GetAllByDateAsync(date).ConfigureAwait(false);

            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async Task<JournalEntryDto> UpdateManyAsync(IEnumerable<JournalEntryDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
