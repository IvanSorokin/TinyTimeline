using System;

namespace Domain.Objects
{
    public class Review
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid Id { get; set; }
    }
}