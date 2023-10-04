using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Employees.Abstractions;

namespace Employees.Middlewares
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IUserService userService) :
            base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length).Trim();
                var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialsAsEncodedString.Split(':');
                if (await _userService.Authenticate(credentials[0], credentials[1]))
                {
                    var claims = new[] { new Claim("name", credentials[0])};
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                }
            }
            Response.StatusCode = 401;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"localhost\"");
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
