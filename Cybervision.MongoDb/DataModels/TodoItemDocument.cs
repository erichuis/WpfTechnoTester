using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cybervision.Dapr.DataModels
{
    public class TodoItemDocument : ISearchable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TodoItemId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? DateStarted{ get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? DateCompleted { get; set; }

        public int InProgress { get; set; }

        public string SearchKey => Title;

        public Guid SearchIdKey => TodoItemId;
    }
}
