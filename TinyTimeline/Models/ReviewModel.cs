using System;

namespace TinyTimeline.Models
{
    public class ReviewModel
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid SessionId { get; set; }
        public Guid Id { get; set; }
    }
}