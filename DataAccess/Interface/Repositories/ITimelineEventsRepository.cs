using System;
using Domain.Objects;

namespace DataAccess.Interface.Repositories
{
    public interface ITimelineEventsRepository
    {
        TimelineEvent Get(Guid id);
        void Save(TimelineEvent timelineEvent);
    }
}