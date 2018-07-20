using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public class TimelineEventModelBuilder : ITimelineEventModelBuilder
    {
        public TimelineEventModel Build(TimelineEvent t, Guid sessionId, bool allowModify) => new TimelineEventModel
                                                            {
                                                                Text = t.Text,
                                                                Date = t.Date,
                                                                Positive = t.Positive,
                                                                Negative = t.Negative,
                                                                Id = t.Id,
                                                                Conclusion = t.Conclusion,
                                                                ToBeDiscussed = t.ToBeDiscussed,
                                                                SessionId = sessionId,
                                                                AllowModify = allowModify
                                                            };

        public IEnumerable<TimelineEventModel> DateSortedBuild(IEnumerable<TimelineEvent> events, Guid sessionId, bool allowModify)
        {
            return events.OrderBy(x => x.Date)
                          .Select(z => Build(z, sessionId, allowModify));
        }
        
        public IEnumerable<TimelineEventModel> DateSortedPositiveBuild(Session session, bool allowModify)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive > 0 && x.Negative == 0), session.Id, allowModify);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedNegativeBuild(Session session, bool allowModify)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive == 0 && x.Negative > 0), session.Id, allowModify);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedDebatableBuild(Session session, bool allowModify)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive > 0 && x.Negative > 0), session.Id, allowModify);
        }
        
        public IEnumerable<TimelineEventModel> ToBeDiscussedBuild(Session session, bool allowModify)
        {
            return session.Events
                          .Where(x => x.ToBeDiscussed > 0)
                          .OrderByDescending(x => x.ToBeDiscussed)
                          .Select(z => Build(z, session.Id, allowModify));
        }
    }
}