using System;
using Domain.Objects;

namespace DataAccess.Interfaces.Repositories
{
    public interface ITimelineEventsRepository
    {
        TimelineEvent Get(Guid id);
        void Save(TimelineEvent timelineEvent);
    }
}