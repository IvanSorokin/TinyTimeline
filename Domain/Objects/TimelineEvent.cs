using System;

namespace Domain.Objects
{
    public class TimelineEvent
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Text { get; set; }
    }
}