using System;
using System.Collections.Generic;
using Domain.Objects;

namespace DataAccess.Interfaces.Repositories
{
    public interface ISessionsRepository
    {
        void Save(Session session);
        void Delete(Guid id);
        void AddEvent(Guid sessionId, TimelineEvent timelineEvent);
        void RemoveEvent(Guid sessionId, Guid eventId);
        void AddReview(Guid sessionId, Review review);
        Session Get(Guid sessionId);
        IEnumerable<Session> GetSessions();
        void Vote(Guid sessionId, Guid eventId, bool isPositive);
        void ToBeDiscussed(Guid sessionId, Guid eventId);
        void SetConclusion(Guid sessionId, Guid eventId, string conclusion);
    }
}