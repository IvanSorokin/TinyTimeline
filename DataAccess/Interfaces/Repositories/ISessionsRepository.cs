using System;
using Domain.Objects;

namespace DataAccess.Interfaces.Repositories
{
    public interface ISessionsRepository
    {
        void Save(Session session);
        void Delete(Guid id);
        void AddEvent(Guid sessionId, TimelineEvent timelineEvent);
        void RemoveEvent(Guid sessionId, Guid eventId);
    }
}