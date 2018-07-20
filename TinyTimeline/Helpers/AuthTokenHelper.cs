using System;
using System.Linq;
using Domain.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TinyTimeline.Helpers
{
    public static class AuthTokenHelper
    {
        private static readonly AuthToken[] authTokens = ReadAuthTokensFromConfig();

        private static AuthToken[] ReadAuthTokensFromConfig() => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
                                                                                           .GetSection("Auth:Tokens")
                                                                                           .Get<ConfigToken[]>()
                                                                                           .Select(z => new AuthToken(z.Value, z.Role))
                                                                                           .ToArray();

        public static bool UserHasRole(UserRole role, HttpContext context) => Guid.TryParse(context.Request.Cookies["authToken"], out Guid parsed) &&
                                                                          authTokens.FirstOrDefault(x => x.Value == parsed)?.Role == role;

        private class ConfigToken
        {
            public Guid Value { get; set; }
            public UserRole Role { get; set; }
        }
    }
}