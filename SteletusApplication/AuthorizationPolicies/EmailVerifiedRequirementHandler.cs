using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace SteletusApplicationPresentation.AuthorizationPolicies
{


    public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EmailVerifiedRequirement requirement)
        {
            var claim = context.User.FindFirst(nameof(SteletusApplicationCore.Models.UserEmailVerificationStatus));

            if (claim != null && claim.Value == ((byte)SteletusApplicationCore.Models.UserEmailVerificationStatus.Verified).ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}

