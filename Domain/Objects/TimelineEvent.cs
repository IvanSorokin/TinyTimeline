using System;

namespace Domain.Objects
{
    public class TimelineEvent
    {
        public Guid Id { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
    }
}