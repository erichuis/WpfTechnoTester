using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public class TodoItemDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
    }
}