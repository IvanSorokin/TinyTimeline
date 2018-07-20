using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TinyTimeline.Helpers;

namespace TinyTimeline.Policies
{
    public class UserRoleRequirementHandler : AuthorizationHandler<UserRoleRequirement>
    {
        private readonly IAuthTokenHelper authTokenHelper;

        public UserRoleRequirementHandler(IAuthTokenHelper authTokenHelper)
        {
            this.authTokenHelper = authTokenHelper;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                       UserRoleRequirement roleRequirement)
        {
            if (!roleRequirement.Roles.Any(x => authTokenHelper.UserHasRole(x)))
            {
                var redirectContext = context.Resource as AuthorizationFilterContext;
                redirectContext.Result = new RedirectToActionResult("SetToken", "Tokens", null);
            }
            context.Succeed(roleRequirement);
            return Task.CompletedTask;
        }
    }
}