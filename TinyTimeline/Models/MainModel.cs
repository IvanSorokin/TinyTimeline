using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class MainModel
    {
        public ICollection<TimelineEventModel> Events { get; set; }
    }
}