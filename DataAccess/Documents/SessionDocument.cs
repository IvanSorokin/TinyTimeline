using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Documents
{
    public class SessionDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        
        [BsonDateTimeOptions(DateOnly = true)] 
        public DateTime CreateDate { get; set; }

        public TimelineEventDocument[] Events { get; set; }       
        
        public string Name { get; set; }
    }
}