﻿using System;
using DataAccess.Documents;
using DataAccess.Interface.Mappers;
using DataAccess.Interface.Repositories;
using Domain.Objects;
using MongoDB.Driver;

namespace DataAccess.Concrete.Repositories
{
    public class TimelineEventsRepository : ITimelineEventsRepository
    {
        private readonly IMongoCollection<TimelineEventDocument> collection;
        private readonly ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper;

        public TimelineEventsRepository(IMongoCollection<TimelineEventDocument> collection, 
                                        ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventsMapper)
        {
            this.collection = collection;
            this.eventsMapper = eventsMapper;
        }

        public TimelineEvent Get(Guid id)
        {
            var result = collection.Find(x => x.Id == id).SingleOrDefault();

            return result != null ? eventsMapper.Map(result) : null;
        }

        public void Save(TimelineEvent timelineEvent)
        {
            collection.ReplaceOne(x => x.Id == timelineEvent.Id, 
                                  eventsMapper.Map(timelineEvent), 
                                  new UpdateOptions {IsUpsert = true});
        }
    }
}