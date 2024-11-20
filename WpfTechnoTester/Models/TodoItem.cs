using System.Text.Json.Serialization;

public class TodoItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }
}