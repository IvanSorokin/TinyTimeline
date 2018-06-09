﻿using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using Domain.Objects;

namespace DataAccess.Concrete.Mappers
{
    public class TimelineEventsMapper : ITwoWayMapper<TimelineEventDocument, TimelineEvent>
    {
        public TimelineEventDocument Map(TimelineEvent obj)
        {
            return new TimelineEventDocument
                   {
                       Id = obj.Id,
                       Text = obj.Text,
                       DateTime = obj.DateTime
                   };
        }

        public TimelineEvent Map(TimelineEventDocument obj)
        {
            return new TimelineEvent
                   {
                       Id = obj.Id,
                       Text = obj.Text,
                       DateTime = obj.DateTime
                   };
        }
    }
}