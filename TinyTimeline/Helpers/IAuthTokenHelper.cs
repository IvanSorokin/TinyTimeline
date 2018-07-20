using Domain.Objects;

namespace TinyTimeline.Helpers
{
    public interface IAuthTokenHelper
    {
        bool UserHasRole(UserRole role);
        bool IsAdmin();
    }
}