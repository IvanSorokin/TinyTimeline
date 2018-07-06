using System;
using System.Collections.Generic;

namespace TinyTimeline.Models
{
    public class ReviewsModel
    {
        public IEnumerable<ReviewModel> Reviews { get; set; }
        public Guid SessionId { get; set; }
    }
}