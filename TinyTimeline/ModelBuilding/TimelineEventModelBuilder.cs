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
                                                                Conclusion = t.Conclusion,
                                                                ToBeDiscussed = t.ToBeDiscussed,
                                                                SessionId = sessionId
                                                            };

        public IEnumerable<TimelineEventModel> DateSortedBuild(IEnumerable<TimelineEvent> events, Guid sessionId)
        {
            return events.OrderBy(x => x.Date)
                          .Select(z => Build(z, sessionId));
        }
        
        public IEnumerable<TimelineEventModel> DateSortedPositiveBuild(Session session)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive > 0 && x.Negative == 0), session.Id);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedNegativeBuild(Session session)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive == 0 && x.Negative > 0), session.Id);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedDebatableBuild(Session session)
        {
            return DateSortedBuild(session.Events.Where(x => x.Positive > 0 && x.Negative > 0), session.Id);
        }
        
        public IEnumerable<TimelineEventModel> ToBeDiscussedBuild(Session session)
        {
            return session.Events
                          .Where(x => x.ToBeDiscussed > 0)
                          .OrderByDescending(x => x.ToBeDiscussed)
                          .Select(z => Build(z, session.Id));
        }
    }
}