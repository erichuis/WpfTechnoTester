using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public class TodoItemDto : ISearchable
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
        public DateTime? DateStarted { get; set; }

        [JsonPropertyName("datecompleted")]
        public DateTime? DateCompleted { get; set; }

        [JsonPropertyName("inprogress")]
        public int InProgress { get; set; }
        [JsonIgnore]
        public string SearchKey => Title;

        [JsonIgnore]
        public Guid SearchIdKey
        {
            get { return TodoItemId; }
            set { TodoItemId = value; }
        }
    }
}
