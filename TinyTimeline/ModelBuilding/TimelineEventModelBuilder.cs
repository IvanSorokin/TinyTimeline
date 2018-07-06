using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public class TimelineEventModelBuilder : ITimelineEventModelBuilder
    {
        public TimelineEventModel Build(TimelineEvent t, Guid sessionId) => new TimelineEventModel
                                                            {
                                                                Text = t.Text,
                                                                Date = t.Date,
                                                                Positive = t.Positive,
                                                                Negative = t.Negative,
                                                                Id = t.Id,
                                                                SessionId = sessionId
                                                            };

        public IEnumerable<TimelineEventModel> DateSortedBuild(Session session)
        {
            return session.Events
                          .OrderBy(x => x.Date)
                          .Select(z => Build(z, session.Id));
        }
        
        public IEnumerable<TimelineEventModel> DateSortedPositiveBuild(Session session)
        {
            return DateSortedBuild(session).Where(x => x.Positive > 0 && x.Negative == 0);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedNegativeBuild(Session session)
        {
            return DateSortedBuild(session).Where(x => x.Positive == 0 && x.Negative > 0);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedDebatableBuild(Session session)
        {
            return DateSortedBuild(session).Where(x => x.Positive > 0 && x.Negative > 0);
        }
    }
}