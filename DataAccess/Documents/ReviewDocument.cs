using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Documents
{
    public class ReviewDocument
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}