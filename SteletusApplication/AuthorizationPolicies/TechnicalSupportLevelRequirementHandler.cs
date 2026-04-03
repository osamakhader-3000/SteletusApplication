using Microsoft.AspNetCore.Authorization;

namespace SteletusApplicationPresentation.AuthorizationPolicies
{
    public class TechnicalSupportLevelRequirementHandler : AuthorizationHandler<TechnicalSupportLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TechnicalSupportLevelRequirement requirement)
        {
            var claim = context.User.FindFirst(nameof(SteletusApplicationCore.Models.UserRole));

            if (claim != null && (claim.Value == ((byte)SteletusApplicationCore.Models.UserRole.Administrator).ToString() || claim.Value ==  ((byte)SteletusApplicationCore.Models.UserRole.TechnicalSupport).ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
