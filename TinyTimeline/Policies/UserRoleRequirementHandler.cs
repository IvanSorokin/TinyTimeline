using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TinyTimeline.Helpers;

namespace TinyTimeline.Policies
{
    public class UserRoleRequirementHandler : AuthorizationHandler<UserRoleRequirement>
    {
        private readonly IAuthTokenHelper authTokenHelper;
        private readonly IHttpContextAccessor contextAccessor;

        public UserRoleRequirementHandler(IAuthTokenHelper authTokenHelper, IHttpContextAccessor contextAccessor)
        {
            this.authTokenHelper = authTokenHelper;
            this.contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                       UserRoleRequirement roleRequirement)
        {
            if (!roleRequirement.Roles.Any(x => authTokenHelper.UserHasRole(x)))
            {
                var redirectContext = context.Resource as AuthorizationFilterContext;
                var returnUrl = contextAccessor.HttpContext.Request.GetUri();
                redirectContext.Result = new RedirectToActionResult("SetToken", "Tokens", new {ReturnUrl = returnUrl.ToString()});
            }
            context.Succeed(roleRequirement);
            return Task.CompletedTask;
        }
    }
}