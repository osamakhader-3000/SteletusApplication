using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using SteletusApplicationPresentation.AuthorizationPolicies;

namespace SteletusApplicationPresentation.Middlewares
{
    public class CustomAuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next,HttpContext context,AuthorizationPolicy policy,PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure?.FailedRequirements.OfType<EmailVerifiedRequirement>().Any() == true)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("{\"error\": \"email_verification_required\"}");
                return;
            }

            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure?.FailedRequirements.OfType<ActiveAccountRequirement>().Any() == true)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("{\"error\": \"active_account_required\"}");
                return;
            }


            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure?.FailedRequirements.OfType<AdminLevelRequirement>().Any() == true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("{\"error\": \"admin_level_required\"}");
                return;
            }

            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure?.FailedRequirements.OfType<TechnicalSupportLevelRequirement>().Any() == true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("{\"error\": \"technical_support_level_required\"}");
                return;
            }


            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
