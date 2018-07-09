using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class ReviewsModel
    {
        public IEnumerable<ReviewModel> Reviews { get; set; }
        public SessionInfoModel SessionInfo { get; set; }
    }
}