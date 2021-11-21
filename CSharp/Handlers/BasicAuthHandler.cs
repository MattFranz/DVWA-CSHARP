using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OWASP10_2021.Models;
using OWASP10_2021.Services;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OWASP10_2021.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            //if (!Request.IsHttps)
            //{
            //    const string insecureProtocolMessage = "Request is HTTP, Basic Authentication will not respond.";
            //    Logger.LogInformation(insecureProtocolMessage);
            //    // 421 Misdirected Request
            //    // The request was directed at a server that is not able to produce a response.
            //    // This can be sent by a server that is not configured to produce responses for the combination of scheme and authority that are included in the request URI.
            //    Response.StatusCode = StatusCodes.Status421MisdirectedRequest;
            //}
            //else
            //{
                Response.StatusCode = 401;
                var headerValue = $"Basic realm=OsCorp";
                Response.Headers.Append(HeaderNames.WWWAuthenticate, headerValue);
            //}

            return Task.CompletedTask;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            User user;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _userService.Authenticate(username, password);

                if (user == null)
                    return await Task.FromResult(AuthenticateResult.Fail("Invalid Credentials"));

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return await Task.FromResult(AuthenticateResult.Fail("Error Occured.Authorization failed."));
            }

        }
    }
}
