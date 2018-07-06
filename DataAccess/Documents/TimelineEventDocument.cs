using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Documents
{
    public class TimelineEventDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonDateTimeOptions(DateOnly = true)] 
        public DateTime Date { get; set; }

        public string Text { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
        public int ToBeDiscussed { get; set; }
        public string Conclusion { get; set; }
    }
}