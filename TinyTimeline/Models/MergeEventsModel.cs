using System;

namespace TinyTimeline.Models
{
    public class MergeEventsModel
    {
        public Guid SessionId { get; set; }
        public Guid[] EventIds { get; set; }
    }
}