using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Authorization
{
    public class HubRequirement : AuthorizationHandler<HubRequirement, HubInvocationContext>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HubRequirement requirement, HubInvocationContext resource)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                context.Succeed(context.PendingRequirements.First());
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
