using System;

namespace DataAccess.Documents
{
    public class ReviewDocument
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}