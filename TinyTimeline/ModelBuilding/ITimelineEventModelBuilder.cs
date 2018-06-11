using System.Collections.Generic;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public interface ITimelineEventModelBuilder
    {
        TimelineEventModel Build(TimelineEvent timelineEvent);
        IEnumerable<TimelineEventModel> DateSortedBuild(IEnumerable<TimelineEvent> events);
    }
}