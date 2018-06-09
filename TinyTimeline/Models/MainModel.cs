using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class MainModel
    {
        public IEnumerable<TimelineEventModel> Events { get; set; }
    }
}