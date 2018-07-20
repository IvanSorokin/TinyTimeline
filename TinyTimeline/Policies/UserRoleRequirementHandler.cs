using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TinyTimeline.Helpers;

namespace TinyTimeline.Policies
{
    public class UserRoleRequirementHandler : AuthorizationHandler<UserRoleRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserRoleRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                       UserRoleRequirement roleRequirement)
        {
            if (!roleRequirement.Roles.Any(x => AuthTokenHelper.UserHasRole(x, httpContextAccessor.HttpContext)))
            {
                var redirectContext = context.Resource as AuthorizationFilterContext;
                redirectContext.Result = new RedirectToActionResult("SetToken", "Tokens", null);
            }
            context.Succeed(roleRequirement);
            return Task.CompletedTask;
        }
    }
}