using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _repository;
        private readonly IMapper _mapper;
        public JournalEntryService(IJournalEntryRepository journalEntryRepository, IMapper mapper) 
        {
            _repository = journalEntryRepository;
            _mapper = mapper;
        }
        public async Task<JournalEntryDto> CreateAsync(JournalEntryDto entity)
        {
            entity.JournalEntryId = Guid.NewGuid();
            var result = await _repository.CreateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id).ConfigureAwait(false);
            return result;
        }

        public async IAsyncEnumerable<JournalEntryDto> FindAllAsync(string search)
        {
            var results = _repository.FindAll(search).ConfigureAwait(false);

            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllAsync()
        {
            var results = _repository.GetAllAsync().ConfigureAwait(false);
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

        public async Task<JournalEntryDto> GetAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> UpdateAsync(JournalEntryDto entity)
        {
            var result = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<JournalEntryDto> UpdateManyAsync(IEnumerable<JournalEntryDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
