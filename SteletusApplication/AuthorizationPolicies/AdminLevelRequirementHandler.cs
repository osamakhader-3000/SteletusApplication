using Microsoft.AspNetCore.Authorization;

namespace SteletusApplicationPresentation.AuthorizationPolicies
{
    public class AdminLevelRequirementHandler : AuthorizationHandler<AdminLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminLevelRequirement requirement)
        {
            var claim = context.User.FindFirst(nameof(SteletusApplicationCore.Models.UserRole));

            if (claim != null && claim.Value == ((byte)SteletusApplicationCore.Models.UserRole.Administrator).ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
