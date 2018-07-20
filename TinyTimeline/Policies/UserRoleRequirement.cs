using Domain.Objects;
using Microsoft.AspNetCore.Authorization;

namespace TinyTimeline.Policies
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        public UserRoleRequirement(UserRole[] roles)
        {
            Roles = roles;
        }

        public UserRole[] Roles { get; }
    }
}