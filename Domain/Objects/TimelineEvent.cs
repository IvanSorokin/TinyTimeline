using System;

namespace Domain.Objects
{
    public class TimelineEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}