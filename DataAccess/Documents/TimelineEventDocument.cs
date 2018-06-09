using System;
using DataAccess.Concrete.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Documents
{
    [CollectionName("timelineEvents")]
    public class TimelineEventDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Text { get; set; }
    }
}