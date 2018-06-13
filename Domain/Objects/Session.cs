using System;

namespace Domain.Objects
{
    public class Session
    {
        public Guid Id { get; set; }
         
        public DateTime CreateDate { get; set; }
        
        public TimelineEvent[] Events { get; set; }
        public string Conclusion { get; set; }
        public string Plans { get; set; }
        public string Name { get; set; }
    }
}