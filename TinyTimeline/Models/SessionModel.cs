using System;

namespace TinyTimeline.Models
{
    public class SessionModel
    {
        public Guid Id { get; set; }
         
        public DateTime Date { get; set; }
        
        public Guid[] EventIds { get; set; }
        public string Conclusion { get; set; }
        public string Plans { get; set; }
    }
}