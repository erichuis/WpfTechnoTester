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
        public bool IsCompleted { get; set; }
    }
}
