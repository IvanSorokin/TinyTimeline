using System;

namespace Domain.Objects
{
    public class TimelineEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
    }
}