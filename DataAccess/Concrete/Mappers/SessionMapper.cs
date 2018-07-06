using System.Linq;
using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using Domain.Objects;

namespace DataAccess.Concrete.Mappers
{
    public class SessionMapper : ITwoWayMapper<SessionDocument, Session>
    {
        private readonly ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventMapper;
        private readonly ITwoWayMapper<ReviewDocument, Review> reviewMapper;

        public SessionMapper(ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventMapper,
                             ITwoWayMapper<ReviewDocument, Review> reviewMapper)
        {
            this.eventMapper = eventMapper;
            this.reviewMapper = reviewMapper;
        }

        public SessionDocument Map(Session obj) => new SessionDocument
        {
            Id = obj.Id,
            CreateDate = obj.CreateDate,
            Events = obj.Events.Select(eventMapper.Map).ToArray(),
            Name = obj.Name,
            Reviews = obj.Reviews.Select(reviewMapper.Map).ToArray()
        };

        public Session Map(SessionDocument obj) => new Session
        {
            Id = obj.Id,
            CreateDate = obj.CreateDate,
            Events = obj.Events.Select(eventMapper.Map).ToArray(),
            Name = obj.Name,
            Reviews = obj.Reviews.Select(reviewMapper.Map).ToArray()
        };
    }
}