using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class JournalEntryService : BaseDataService<JournalEntryDto, JournalEntryDataService>, IJournalEntryService
    {
        private readonly IJournalEntryDataService _dataService;
        private readonly IMapper _mapper;
        public JournalEntryService(JournalEntryDataService dataService, IMapper mapper) :base(dataService, mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search)
        {
            var results = _dataService.FindAllAsync(search).ConfigureAwait(false);

            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<JournalEntryDto> UpdateManyAsync(IEnumerable<JournalEntryDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
