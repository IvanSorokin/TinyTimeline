using System.Linq;
using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public class SessionModelBuilder : ISessionModelBuilder
    {
        private readonly ITimelineEventModelBuilder eventModelBuilder;

        public SessionModelBuilder(ITimelineEventModelBuilder eventModelBuilder)
        {
            this.eventModelBuilder = eventModelBuilder;
        }

        public SessionModel Build(Session session)
        {
            return new SessionModel
                   {
                       SessionId = session.Id,
                       CreateDate = session.CreateDate,
                       Events = session.Events.Select(eventModelBuilder.Build).ToArray(),
                       Conclusion = session.Conclusion,
                       Plans = session.Plans,
                       Name = session.Name
                   };
        }
    }
}