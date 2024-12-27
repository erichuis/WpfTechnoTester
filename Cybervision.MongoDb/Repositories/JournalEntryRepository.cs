using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Cybervision.Dapr.Services
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly IMongoCollection<JournalEntryDocument> _journalEntries;
        private readonly IMapper _mapper;

        public JournalEntryRepository(IConfiguration config, IMapper mapper)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
           
            _journalEntries = database.GetCollection<JournalEntryDocument>("JournalEntries");
            _mapper = mapper;
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllAsync()
        {
            var list = await _journalEntries.Find(entry => true).ToListAsync();
            
            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByCategoryAsync(string category)
        {
            var list = await _journalEntries.Find(journalEntry => journalEntry.Category == category).ToListAsync();
            
            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> GetAllByDateAsync(DateTime date)
        {
            var list = await _journalEntries.Find(journalEntry => journalEntry.DateEntry == date).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async IAsyncEnumerable<JournalEntryDto> FindAll(string search)
        {
            var list = await _journalEntries.Find(journalEntry => journalEntry.Entry.Contains(search, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<JournalEntryDto>(item);
            }
        }

        public async Task<JournalEntryDto> GetByIdAsync(Guid id) 
        {
            var journalEntry = await _journalEntries.Find(task => task.JournalEntryId == id).FirstOrDefaultAsync();
            return _mapper.Map<JournalEntryDto>(journalEntry);
        }

        public async Task<JournalEntryDto> CreateAsync(JournalEntryDto journalEntry)
        {
            var journalEntryDocument = _mapper.Map<JournalEntryDocument>(journalEntry);
            await _journalEntries.InsertOneAsync(journalEntryDocument, new InsertOneOptions { BypassDocumentValidation = true });
            //Todo retrieve Id

            return journalEntry;
        }

        public async Task<bool> UpdateAsync(JournalEntryDto updatedjournalEntry)
        {
            var journalEntryDocument = _mapper.Map<JournalEntryDocument>(updatedjournalEntry);
            var result = await _journalEntries.ReplaceOneAsync(journalEntry => journalEntry.JournalEntryId == updatedjournalEntry.JournalEntryId, journalEntryDocument);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _journalEntries.DeleteOneAsync(journalEntry => journalEntry.JournalEntryId == id);
            return result.IsAcknowledged;
        }
    }
}
