using System;

namespace TinyTimeline.Models
{
    public class VoteModel
    {
        public Guid EventId { get; set; }
        public bool IsPositive { get; set; }
        public Guid SessionId { get; set; }
    }
}