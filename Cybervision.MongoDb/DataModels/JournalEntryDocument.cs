using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cybervision.Dapr.DataModels
{
    public class JournalEntryDocument : ISearchable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid JournalEntryId { get; set; }
        public required string Entry { get; set; }

        public string? Category { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DateEntry { get; set; }

        public string SearchKey => Entry;

        public Guid SearchIdKey => JournalEntryId;
    }
}
