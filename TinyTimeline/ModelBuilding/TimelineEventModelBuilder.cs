using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public class TimelineEventModelBuilder : ITimelineEventModelBuilder
    {
        public IEnumerable<TimelineEventModel> BuildDateSorted(IEnumerable<TimelineEvent> events, Guid sessionId, bool allowModify)
        {
            return events.OrderBy(x => x.Date)
                         .Select(z => Build(z, sessionId, allowModify));
        }

        public IEnumerable<TimelineEventModel> BuildPositive(Session session, bool allowModify)
        {
            return BuildSortedDescending(session.Events.Where(x => x.Positive > 0 && x.Negative == 0), 
                                         session.Id, 
                                         allowModify, 
                                         z => z.Positive);
        }

        public IEnumerable<TimelineEventModel> BuildNegative(Session session, bool allowModify)
        {
            return BuildSortedDescending(session.Events.Where(x => x.Positive == 0 && x.Negative > 0), 
                                         session.Id, 
                                         allowModify,
                                         z => z.Negative);
        }

        public IEnumerable<TimelineEventModel> BuildToBeDiscussed(Session session, bool allowModify)
        {
            return BuildSortedDescending(session.Events.Where(x => x.ToBeDiscussed > 0), 
                                         session.Id, 
                                         allowModify,
                                         z => z.ToBeDiscussed);
        }
        
        public IEnumerable<TimelineEventModel> BuildDebatable(Session session, bool allowModify)
        {
            return BuildDateSorted(session.Events.Where(x => x.Positive > 0 && x.Negative > 0), 
                                   session.Id, 
                                   allowModify);
        }

        private TimelineEventModel Build(TimelineEvent t, Guid sessionId, bool allowModify) => new TimelineEventModel
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

        private IEnumerable<TimelineEventModel> BuildSortedDescending(IEnumerable<TimelineEvent> events,
                                                                      Guid sessionId,
                                                                      bool allowModify,
                                                                      Func<TimelineEvent, IComparable> selector)
        {
            return events.OrderByDescending(selector)
                         .Select(z => Build(z, sessionId, allowModify));
        }
    }
}