using System;

namespace TinyTimeline.Models
{
    public class TimelineEventModel
    {
        public DateTimeOffset DateTime { get; set; }
        public string Text { get; set; }
    }
}