using Microsoft.AspNetCore.Authentication;
using System;

namespace OWASP10_2021.Services
{
    public static class BasicAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
            => builder.AddBasic("Basic");

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddBasic(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<BasicAuthenticationOptions> configureOptions)
            => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddBasic(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<BasicAuthenticationOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}
