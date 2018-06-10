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
    public class TimelineEventsRepository : ITimelineEventsRepository
    {
        private readonly IMongoCollection<TimelineEventDocument> collection;
        private readonly ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper;

        private static UpdateDefinition<TimelineEventDocument> positiveInc;
        private static UpdateDefinition<TimelineEventDocument> negativeInc;

        public TimelineEventsRepository(IMongoCollection<TimelineEventDocument> collection,
                                        ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper)
        {
            this.collection = collection;
            this.eventsMapper = eventsMapper;

            positiveInc = new UpdateDefinitionBuilder<TimelineEventDocument>().Inc<int>(x => x.Positive, 1);
            negativeInc = new UpdateDefinitionBuilder<TimelineEventDocument>().Inc<int>(x => x.Negative, 1);
        }

        public TimelineEvent Get(Guid id)
        {
            var result = collection.Find(x => x.Id == id).SingleOrDefault();

            return result != null ? eventsMapper.Map(result) : null;
        }

        public IEnumerable<TimelineEvent> GetAll() => collection.Find(_ => true).ToEnumerable().Select(eventsMapper.Map);

        public void Save(TimelineEvent timelineEvent)
        {
            collection.ReplaceOne(x => x.Id == timelineEvent.Id,
                                  eventsMapper.Map(timelineEvent),
                                  new UpdateOptions { IsUpsert = true });
        }

        public void Vote(Guid eventId, bool isPositive)
        {
            collection.UpdateOne(x => x.Id == eventId, isPositive ? positiveInc : negativeInc);
        }
    }
}