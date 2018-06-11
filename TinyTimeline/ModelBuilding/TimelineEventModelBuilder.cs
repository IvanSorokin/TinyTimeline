﻿using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public class TimelineEventModelBuilder : ITimelineEventModelBuilder
    {
        public TimelineEventModel Build(TimelineEvent t) => new TimelineEventModel
                                                            {
                                                                Text = t.Text,
                                                                Date = t.Date,
                                                                Positive = t.Positive,
                                                                Negative = t.Negative,
                                                                Id = t.Id
                                                            };

        public IEnumerable<TimelineEventModel> DateSortedBuild(IEnumerable<TimelineEvent> events)
        {
            return events.OrderBy(x => x.Date)
                         .Select(Build);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedPositiveBuild(IEnumerable<TimelineEvent> events)
        {
            return DateSortedBuild(events).Where(x => x.Positive > 0 && x.Negative == 0);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedNegativeBuild(IEnumerable<TimelineEvent> events)
        {
            return DateSortedBuild(events).Where(x => x.Positive == 0 && x.Negative > 0);
        }
        
        public IEnumerable<TimelineEventModel> DateSortedDebatableBuild(IEnumerable<TimelineEvent> events)
        {
            return DateSortedBuild(events).Where(x => x.Positive > 0 && x.Negative > 0);
        }
    }
}