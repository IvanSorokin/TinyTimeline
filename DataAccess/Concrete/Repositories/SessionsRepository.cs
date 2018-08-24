using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using DataAccess.Interfaces.Repositories;
using Domain.Objects;
using MongoDB.Driver;

namespace DataAccess.Concrete.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly IMongoCollection<SessionDocument> collection;
        private readonly ITwoWayMapper<SessionDocument, Session> sessionMapper;
        private static UpdateDefinition<SessionDocument> positiveInc;
        private static UpdateDefinition<SessionDocument> negativeInc;
        private readonly ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper;
        private readonly ITwoWayMapper<ReviewDocument, Review> reviewMapper;
        private static ProjectionDefinition<SessionDocument> sessionInfoFields;

        public SessionsRepository(IMongoCollection<SessionDocument> collection,
                                  ITwoWayMapper<SessionDocument, Session> sessionMapper,
                                  ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper,
                                  ITwoWayMapper<ReviewDocument, Review> reviewMapper)
        {
            this.collection = collection;
            this.sessionMapper = sessionMapper;
            this.eventsMapper = eventsMapper;
            this.reviewMapper = reviewMapper;

            positiveInc = Builders<SessionDocument>.Update.Inc(x => x.Events[-1].Positive, 1);
            negativeInc = Builders<SessionDocument>.Update.Inc(x => x.Events[-1].Negative, 1);
            sessionInfoFields = Builders<SessionDocument>.Projection.Include(x => x.CreateDate)
                                                                    .Include(x => x.Name)
                                                                    .Include(x => x.Id);
        }

        public void Save(Session session)
        {
            collection.ReplaceOne(x => x.Id == session.Id,
                                  sessionMapper.Map(session),
                                  new UpdateOptions {IsUpsert = true});
        }

        public void DeleteSession(Guid id)
        {
            collection.DeleteOne(x => x.Id == id);
        }

        public void AddEvent(Guid sessionId, TimelineEvent doc)
        {
            var update = Builders<SessionDocument>.Update.AddToSet(x => x.Events, eventsMapper.Map(doc));
            collection.UpdateOne(x => x.Id == sessionId, update);
        }

        public void AddReview(Guid sessionId, Review review)
        {
            var update = Builders<SessionDocument>.Update.AddToSet(x => x.Reviews, reviewMapper.Map(review));
            collection.UpdateOne(x => x.Id == sessionId, update);
        }

        public void RemoveReview(Guid sessionId, Guid reviewId)
        {
            var update = Builders<SessionDocument>.Update.PullFilter(x => x.Reviews, y => y.Id == reviewId);
            collection.UpdateOne(x => x.Id == sessionId, update);
        }

        public void RemoveEvent(Guid sessionId, Guid eventId)
        {
            var update = Builders<SessionDocument>.Update.PullFilter(x => x.Events, y => y.Id == eventId);
            collection.UpdateOne(x => x.Id == sessionId, update);
        }
        
        public Session Get(Guid sessionId)
        {
            return sessionMapper.Map(collection.Find(x => x.Id == sessionId).Single());
        }
        
        public IEnumerable<Session> GetSessions()
        {
            return collection.Find(_ => true).ToEnumerable().Select(sessionMapper.Map);
        }
        
        public void Vote(Guid sessionId, Guid eventId, bool isPositive)
        {
            var filter = Builders<SessionDocument>.Filter.Where(x => x.Id == sessionId && x.Events.Any(i => i.Id == eventId));
            collection.UpdateOne(filter, isPositive ? positiveInc : negativeInc);
        }
        
        public void ToBeDiscussed(Guid sessionId, Guid eventId)
        {
            var filter = Builders<SessionDocument>.Filter.Where(x => x.Id == sessionId && x.Events.Any(i => i.Id == eventId));
            collection.UpdateOne(filter, Builders<SessionDocument>.Update.Inc(x => x.Events[-1].ToBeDiscussed, 1));
        }

        public void SetConclusion(Guid sessionId, Guid eventId, string conclusion)
        {
            var filter = Builders<SessionDocument>.Filter.Where(x => x.Id == sessionId && x.Events.Any(i => i.Id == eventId));
            collection.UpdateOne(filter, Builders<SessionDocument>.Update.Set(x => x.Events[-1].Conclusion, conclusion));
        }

        public SessionInfo GetSessionInfo(Guid sessionId)
        {
            var doc = collection.Find(x => x.Id == sessionId).Project<SessionDocument>(sessionInfoFields).Single();
            return new SessionInfo
                   {
                       Name = doc.Name,
                       CreateDate = doc.CreateDate,
                       Id = doc.Id
                   };
        }

        public void MergeEvents(Guid sessionId, Guid[] eventIds)
        {
            var session = collection.Find(x => x.Id == sessionId).Single();
            var eventsToMerge = session.Events.Where(x => eventIds.Contains(x.Id)).ToList();

            var mergedEvent = new TimelineEvent
                             {
                                 Date = DateTime.Now.Date,
                                 Id = Guid.NewGuid(),
                                 Negative = eventsToMerge.Select(x => x.Negative).Sum(),
                                 Positive = eventsToMerge.Select(x => x.Positive).Sum(),
                                 ToBeDiscussed = eventsToMerge.Select(x => x.ToBeDiscussed).Sum(),
                                 Text = string.Join("\n", eventsToMerge.Select(x => "* " + x.Text)),
                                 Conclusion = string.Join("\n", eventsToMerge.Select(x => x.Conclusion))
                             };
            
            AddEvent(sessionId, mergedEvent);
            
            var update = Builders<SessionDocument>.Update.PullFilter(x => x.Events, y => eventIds.Contains(y.Id));
            collection.UpdateOne(x => x.Id == sessionId, update); 
        }
    }
}