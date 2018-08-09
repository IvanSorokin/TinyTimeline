using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class SessionModel
    {       
        public ICollection<TimelineEventModel> Events { get; set; }
        
        public EventFilterType EventFilterType { get; set; }
        
        public SessionInfoModel SessionInfo { get; set; }
    }
}