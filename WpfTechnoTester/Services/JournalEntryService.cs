using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;
using WpfTechnoTester.Clients;

namespace WpfTechnoTester.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IMapper _mapper;
        private readonly IHttpJournalEntryClient _httpAppClient;

        public JournalEntryService(IMapper mapper, IHttpJournalEntryClient appClient) 
        {
            _mapper = mapper;
            _httpAppClient = appClient;
        }
        public async Task<JournalEntry> CreateAsync(JournalEntry entity)
        {
            var dto = _mapper.Map<JournalEntryDto>(entity);
            var result = await _httpAppClient.CreateAsync(dto).ConfigureAwait(false);
            return _mapper.Map<JournalEntry>(result);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _httpAppClient.DeleteAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            var result = await _httpAppClient.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<JournalEntry>>( result);
        }

        public async Task<JournalEntry> GetAsync(Guid id)
        {
            var result = await _httpAppClient.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<JournalEntry>(result);
        }

        public async Task<bool> UpdateAsync(JournalEntry entity)
        {
            var dto = _mapper.Map<JournalEntryDto>(entity);
            var result = await _httpAppClient.UpdateAsync(dto).ConfigureAwait(false);

            return result;
        }

        public Task<JournalEntry> UpdateManyAsync(IEnumerable<JournalEntry> entities)
        {
            throw new NotImplementedException();
        }
    }
}
