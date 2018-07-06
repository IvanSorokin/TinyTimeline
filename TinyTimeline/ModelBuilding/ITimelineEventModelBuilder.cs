using System;
using System.Collections.Generic;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public interface ITimelineEventModelBuilder
    {
        TimelineEventModel Build(TimelineEvent t, Guid sessionId);
        IEnumerable<TimelineEventModel> DateSortedBuild(Session session);
        IEnumerable<TimelineEventModel> DateSortedPositiveBuild(Session session);
        IEnumerable<TimelineEventModel> DateSortedNegativeBuild(Session session);
        IEnumerable<TimelineEventModel> DateSortedDebatableBuild(Session session);
    }
}