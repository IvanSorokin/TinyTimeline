using System.Linq;
using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using Domain.Objects;

namespace DataAccess.Concrete.Mappers
{
    public class SessionMapper : ITwoWayMapper<SessionDocument, Session>
    {
        private readonly ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventMapper;

        public SessionMapper(ITwoWayMapper<TimelineEventDocument, TimelineEvent> eventMapper)
        {
            this.eventMapper = eventMapper;
        }

        public SessionDocument Map(Session obj)
        {
            return new SessionDocument
                   {
                       Id = obj.Id,
                       CreateDate = obj.CreateDate,
                       Events = obj.Events.Select(eventMapper.Map).ToArray(),
                       Conclusion = obj.Conclusion,
                       Plans = obj.Plans,
                       Name = obj.Name
                   };
        }
        
        public Session Map(SessionDocument obj)
        {
            return new Session
                   {
                       Id = obj.Id,
                       CreateDate = obj.CreateDate,
                       Events = obj.Events.Select(eventMapper.Map).ToArray(),
                       Conclusion = obj.Conclusion,
                       Plans = obj.Plans,
                       Name = obj.Name
                   };
        }
    }
}