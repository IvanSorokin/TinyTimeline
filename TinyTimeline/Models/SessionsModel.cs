using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class SessionsModel
    {
        public IEnumerable<SessionModel> Sessions;
        public bool AllowModify { get; set; }
    }
}