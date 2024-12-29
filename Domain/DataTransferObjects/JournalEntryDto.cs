using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public class JournalEntryDto : ISearchable
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("journalentryid")]
        public required Guid JournalEntryId { get; set; }

        [JsonPropertyName("entry")]
        public required string Entry { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("dateentry")]
        public DateTime DateStarted { get; set; }

        public string SearchKey => Entry;

        public Guid SearchIdKey => JournalEntryId;
    }
}