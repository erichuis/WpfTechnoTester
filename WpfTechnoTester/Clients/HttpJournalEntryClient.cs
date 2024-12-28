using Domain.DataTransferObjects;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace WpfTechnoTester.Clients
{
    class HttpJournalEntryClient : IHttpJournalEntryClient
    {
        private readonly IHttpAuthenticationClient _httpAuthenticationClient;
        private readonly HttpClient _httpClient;
        public HttpJournalEntryClient(IHttpAuthenticationClient httpAuthenticationClient) 
        {
            _httpAuthenticationClient = httpAuthenticationClient;
            _httpClient = _httpAuthenticationClient.Client;
            
        }

        public async Task<IEnumerable<JournalEntryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("JournalEntry/GetAllJournalEntries");

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var entries = JsonSerializer.Deserialize<List<JournalEntryDto>>(json);
            if (entries == null)
            {
                return [];
            }
            return entries;
        }

        public async Task<JournalEntryDto> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"JournalEntry/GetJournalEntry{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var entry = JsonSerializer.Deserialize<JournalEntryDto>(json);

            if (entry == null)
            {
                throw new Exception("No TodoItem found");
            }
            return entry;
        }

        public async Task<IEnumerable<JournalEntryDto>> GetAllByCategoryAsync(string category)
        {
            var response = await _httpClient.GetAsync("JournalEntry/GetAllJournalEntriesByCategory{category}");

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var entries = JsonSerializer.Deserialize<List<JournalEntryDto>>(json);
            if (entries == null)
            {
                return [];
            }
            return entries;
        }

        public async Task<IEnumerable<JournalEntryDto>> GetAllByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync("JournalEntry/GetAllJournalEntriesByDate{category}");

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var entries = JsonSerializer.Deserialize<List<JournalEntryDto>>(json);
            if (entries == null)
            {
                return [];
            }
            return entries;
        }

        public async Task<IEnumerable<JournalEntryDto>> FindAll(string search)
        {
            var response = await _httpClient.GetAsync("JournalEntry/FindAllJournalEntriesSearch{search}");

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                return [];
            }
            var entries = JsonSerializer.Deserialize<List<JournalEntryDto>>(json);
            if (entries == null)
            {
                return [];
            }
            return entries;
        }

        public async Task<JournalEntryDto> CreateAsync(JournalEntryDto entry)
        {
            var response = await _httpClient.PostAsJsonAsync($"JournalEntry/CreateJournalEntry", entry).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var newItem = JsonSerializer.Deserialize<JournalEntryDto>(json);
            if (newItem == null)
            {
                throw new Exception("No journal entry created");
            }
            return newItem;
        }

        public async Task<bool> UpdateAsync(JournalEntryDto updatedEntry)
        {
            ArgumentNullException.ThrowIfNull(updatedEntry);

            // Send the PUT request
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"JournalEntry/UpdateJournalEntry", updatedEntry).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No journal entry updated");
            }
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"JournalEntry/DeleteJournalEntry/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(json);
            if (!result)
            {
                throw new Exception("No entry found");
            }
            return result;
        }
    }
}
