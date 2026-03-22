using Microsoft.AspNetCore.Authorization;

namespace SteletusApplicationPresentation.AuthorizationPolicies
{
    public class ActiveAccountHandler : AuthorizationHandler<ActiveAccountRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ActiveAccountRequirement requirement)
        {
            var claim = context.User.FindFirst(nameof(SteletusApplicationCore.Models.UserAccountStatus));

            if (claim != null && claim.Value == ((byte)SteletusApplicationCore.Models.UserAccountStatus.Active).ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
