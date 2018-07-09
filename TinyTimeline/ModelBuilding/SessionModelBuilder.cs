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
                       Events = eventModelBuilder.DateSortedBuild(session.Events, session.Id),
                       SessionInfo = new SessionInfoModel {SessionId = session.Id, SessionCreateDate = session.CreateDate, SessionName = session.Name}
                   };
        }
    }
}