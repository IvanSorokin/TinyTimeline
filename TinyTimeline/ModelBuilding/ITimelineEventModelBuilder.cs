using System;
using System.Collections.Generic;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public interface ITimelineEventModelBuilder
    {
        IEnumerable<TimelineEventModel> DateSortedBuild(IEnumerable<TimelineEvent> events, Guid sessionId, bool allowModify);
        IEnumerable<TimelineEventModel> DateSortedPositiveBuild(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> DateSortedNegativeBuild(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> DateSortedDebatableBuild(Session session, bool allowModify);
        IEnumerable<TimelineEventModel> ToBeDiscussedBuild(Session session, bool allowModify);
    }
}