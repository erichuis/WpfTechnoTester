using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Cybervision.Dapr.Repositories
{
    public class JournalEntryRepository : BaseRepository<JournalEntryDto, JournalEntryDocument>, IJournalEntryRepository
    {
        private IMapper _mapper;
        public JournalEntryRepository(IConfiguration config, IMapper mapper) : base(config, mapper, "JournalEntries")
        {
            _mapper = mapper;
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category)
        {
            var list = await _collection.Find(item => item.Category == null ? false : item.Category.Equals(category)).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date)
        {
            var list = await _collection.Find(item => item.DateEntry.Equals(date)).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> FindAll(string search)
        {
            var list = await _collection.Find(item => item.Entry.Contains(search)).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }
    }
}
