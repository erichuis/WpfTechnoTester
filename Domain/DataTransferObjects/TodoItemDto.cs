using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public class TodoItemDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("todoitemid")]
        public required Guid TodoItemId { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("datestarted")]
        public DateTime DateStarted { get; set; }

        [JsonPropertyName("datecompleted")]
        public DateTime DateCompleted { get; set; }

        [JsonPropertyName("inprogress")]
        public bool InProgress { get; set; }
    }
}