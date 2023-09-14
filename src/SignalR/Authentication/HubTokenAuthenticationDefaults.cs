using Microsoft.AspNetCore.Authentication;

namespace SignalR.Authentication
{
    public static class HubTokenAuthenticationDefaults
    {
        public const string AuthenticationScheme = "HubTokenAuthentication";

        public static AuthenticationBuilder AddHubTokenAuthenticationScheme(this AuthenticationBuilder builder)
        {
            return AddHubTokenAuthenticationScheme(builder, (options) => { });
        }

        public static AuthenticationBuilder AddHubTokenAuthenticationScheme(this AuthenticationBuilder builder, Action<HubTokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<HubTokenAuthenticationOptions, HubTokenAuthenticationHandler>(AuthenticationScheme, configureOptions);
        }
    }
}
