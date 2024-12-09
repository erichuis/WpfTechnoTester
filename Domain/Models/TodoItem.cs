using System.Text.Json.Serialization;

namespace Domain.Models
{
    //Todo...nog nodig?
    public class TodoItem
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