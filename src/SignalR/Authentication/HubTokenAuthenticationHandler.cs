using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SignalR.Authentication
{
    public class HubTokenAuthenticationHandler : AuthenticationHandler<HubTokenAuthenticationOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }

        public HubTokenAuthenticationHandler(
          IOptionsMonitor<HubTokenAuthenticationOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock,
          IServiceProvider serviceProvider)
          : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // get access token from query string
            var token = Request.Query["access_token"];

            // implement logic to authenticate
            if (!string.IsNullOrWhiteSpace(token) && token.Equals("mytoken"))
            {
                var claims = new[] { new Claim("token", token) };
                var identity = new ClaimsIdentity(claims, nameof(HubTokenAuthenticationHandler));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            return Task.FromResult(AuthenticateResult.Fail("No access token provided."));
        }
    }
}
