using System;

namespace DataAccess.Documents
{
    public class TimelineEventDocument
    {
        public Guid Id { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
    }
}