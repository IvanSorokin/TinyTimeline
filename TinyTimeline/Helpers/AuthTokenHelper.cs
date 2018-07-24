using System;
using System.Linq;
using Domain.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TinyTimeline.Helpers
{
    public class AuthTokenHelper : IAuthTokenHelper
    {
        private readonly AuthToken[] authTokens;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthTokenHelper(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            authTokens = ReadAuthTokensFromConfig();
        }

        private HttpContext context => httpContextAccessor.HttpContext;

        public bool UserHasRole(UserRole role) => Guid.TryParse(context.Request.Cookies["authToken"], out Guid parsed) &&
                                                  authTokens.FirstOrDefault(x => x.Value == parsed)?.Role == role;

        public bool IsAdmin() => UserHasRole(UserRole.Administrator);

        private AuthToken[] ReadAuthTokensFromConfig() => configuration
                                                          .GetSection("Auth:Tokens")
                                                          .Get<ConfigToken[]>()
                                                          .Select(z => new AuthToken(z.Value, z.Role))
                                                          .ToArray();

        private class ConfigToken
        {
            public Guid Value { get; set; }
            public UserRole Role { get; set; }
        }
    }
}