using Domain.Objects;
using TinyTimeline.Models;

namespace TinyTimeline.ModelBuilding
{
    public interface ISessionModelBuilder
    {
        SessionModel Build(Session session);
    }
}