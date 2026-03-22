using Microsoft.AspNetCore.Authorization;

namespace SteletusApplicationPresentation.AuthorizationPolicies
{
    public class AdminLevelRequirementHandler : AuthorizationHandler<AdminLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminLevelRequirement requirement)
        {
            var claim = context.User.FindFirst(nameof(SteletusApplicationCore.Models.User.SecurityLevel));

            if (claim != null && byte.TryParse(claim.Value, out byte result) && result > 1)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
