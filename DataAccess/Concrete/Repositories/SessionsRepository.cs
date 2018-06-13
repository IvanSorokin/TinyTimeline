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

        public SessionsRepository(IMongoCollection<SessionDocument> collection,
                                  ITwoWayMapper<SessionDocument, Session> sessionMapper,
                                  ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper)
        {
            this.collection = collection;
            this.sessionMapper = sessionMapper;
            this.eventsMapper = eventsMapper;

            positiveInc = Builders<SessionDocument>.Update.Inc(x => x.Events[-1].Positive, 1);
            negativeInc = Builders<SessionDocument>.Update.Inc(x => x.Events[-1].Negative, 1);
        }

        public void Save(Session session)
        {
            collection.ReplaceOne(x => x.Id == session.Id,
                                  sessionMapper.Map(session),
                                  new UpdateOptions {IsUpsert = true});
        }

        public void Delete(Guid id)
        {
            collection.DeleteOne(x => x.Id == id);
        }

        public void AddEvent(Guid sessionId, TimelineEvent doc)
        {
            var update = Builders<SessionDocument>.Update.AddToSet(x => x.Events, eventsMapper.Map(doc));
            collection.UpdateOne(x => x.Id == sessionId, update);
        }

        public void RemoveEvent(Guid sessionId, Guid eventId)
        {
            var update = Builders<SessionDocument>.Update.PullFilter(x => x.Events, y => y.Id == eventId);
            collection.UpdateOne(x => x.Id == sessionId, update);
        }
        
        public IEnumerable<TimelineEvent> GetAllEvents(Guid sessionId)
        {
            return collection.Find(x => x.Id == sessionId).Single().Events.Select(eventsMapper.Map);
        }
        
        public void Vote(Guid sessionId, Guid eventId, bool isPositive)
        {
            var filter = Builders<SessionDocument>.Filter.Where(x => x.Id == sessionId && x.Events.Any(i => i.Id == eventId));
            collection.UpdateOne(filter, isPositive ? positiveInc : negativeInc);
        }
    }
}