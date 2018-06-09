using System;
using System.Collections.Generic;
using Domain.Objects;

namespace DataAccess.Interfaces.Repositories
{
    public interface ITimelineEventsRepository
    {
        TimelineEvent Get(Guid id);
        IEnumerable<TimelineEvent> GetAll();
        void Save(TimelineEvent timelineEvent);
    }
}