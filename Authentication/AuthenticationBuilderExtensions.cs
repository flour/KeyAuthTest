using System;
using Microsoft.AspNetCore.Authentication;

namespace ApiKeyTest.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder builder, Action<AuthenticationOptions> options = null)
            => builder.AddScheme<AuthenticationOptions, AuthenticationHandler>(AuthenticationOptions.DefaultScheme, options ?? (opts => { }));
    }
}