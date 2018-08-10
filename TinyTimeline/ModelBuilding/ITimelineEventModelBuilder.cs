using System;
using System.Collections.Generic;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public interface ITimelineEventModelBuilder
    {
        IEnumerable<TimelineEventModel> BuildDateSorted(IEnumerable<TimelineEvent> events, Guid sessionId, bool allowModify);
        IEnumerable<TimelineEventModel> BuildPositive(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> BuildNegative(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> BuildDebatable(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> BuildToBeDiscussed(Session session, bool allowModify);
    }
}