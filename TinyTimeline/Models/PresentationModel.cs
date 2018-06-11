using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class PresentationModel
    {
        public ICollection<TimelineEventModel> Events { get; set; }
    }
}