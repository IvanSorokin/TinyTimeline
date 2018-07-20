using System;
using System.Linq;
using Domain.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TinyTimeline.Helpers
{
    public class AuthTokenHelper : IAuthTokenHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private HttpContext context => httpContextAccessor.HttpContext;
        
        private static readonly AuthToken[] authTokens = ReadAuthTokensFromConfig();

        public AuthTokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private static AuthToken[] ReadAuthTokensFromConfig() => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
                                                                                           .GetSection("Auth:Tokens")
                                                                                           .Get<ConfigToken[]>()
                                                                                           .Select(z => new AuthToken(z.Value, z.Role))
                                                                                           .ToArray();

        public bool UserHasRole(UserRole role) => Guid.TryParse(context.Request.Cookies["authToken"], out Guid parsed) &&
                                                                        authTokens.FirstOrDefault(x => x.Value == parsed)?.Role == role;

        public bool IsAdmin() => UserHasRole(UserRole.Administrator);

        private class ConfigToken
        {
            public Guid Value { get; set; }
            public UserRole Role { get; set; }
        }
    }
}