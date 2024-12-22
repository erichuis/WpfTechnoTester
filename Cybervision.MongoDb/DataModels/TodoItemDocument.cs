using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cybervision.Dapr.DataModels
{
    public class TodoItemDocument
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

        public bool InProgress { get; set; }
    }
}
