using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationPresentation.DataTransferObjects;
using SteletusApplicationPresentation.Mappers;
using System.Security.Claims;

namespace SteletusApplicationPresentation.APIs
{
    public static class Builds
    {
        public static void Map(RouteGroupBuilder apiGroup)
        {
            RouteGroupBuilder buildGroup = apiGroup.MapGroup("/builds").RequireAuthorization("VerifiedEmail", "ActiveAccount");
            buildGroup.MapGet("/list", ListBuilds);
            buildGroup.MapGet("/get/{id}", GetBuild).RequireAuthorization("TechnicalSupportLevel");
            buildGroup.MapPost("/create", CreateBuild).RequireAuthorization("TechnicalSupportLevel");
            buildGroup.MapPut("/update", UpdateBuild).RequireAuthorization("TechnicalSupportLevel"); ;
            buildGroup.MapDelete("/delete/{id}", DeleteBuild).RequireAuthorization("TechnicalSupportLevel");
        }

        private static async Task<Ok<List<Build>>> ListBuilds(IBuildService buildService)
        {
            IEnumerable<SteletusApplicationCore.Models.Build> result = await buildService.ListBuilds();
            return TypedResults.Ok(result.ToDtoList().ToList());
        }

        private static async Task<Results<Ok<Build>, NotFound>> GetBuild(IBuildService buildService, [FromRoute] Guid id)
        {
            SteletusApplicationCore.Models.Build? result = await buildService.GetBuildById(id);
            if (result == null)
                return TypedResults.NotFound();
            return TypedResults.Ok(result.ToDto());
        }

        private static async Task<Results<Ok, InternalServerError<string>>> CreateBuild(HttpContext httpContext, IBuildService buildService, [FromBody] Build build)
        {
            string? userIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.InternalServerError("User ID claim not found");
            }

            Guid userId = Guid.Parse(userIdClaim);
            build.UpdatedBy = userId;
            int result = await buildService.CreateBuild(build.ToDomain());
            if (result == 0)
                return TypedResults.InternalServerError("Build creation failed");
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, InternalServerError<string>>> UpdateBuild(HttpContext httpContext, IBuildService buildService, [FromBody] Build build)
        {
            string? userIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.InternalServerError("User ID claim not found");
            }

            Guid userId = Guid.Parse(userIdClaim);
            build.UpdatedBy = userId;

            await buildService.UpdateBuild(build.ToDomain());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, NotFound, InternalServerError<string>>> DeleteBuild(HttpContext httpContext,IBuildService buildService, [FromRoute] Guid id)
        {
            Build build = new() { Id = id };

            string? userIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.InternalServerError("User ID claim not found");
            }

            Guid userId = Guid.Parse(userIdClaim);
            build.UpdatedBy = userId;

            int result = await buildService.DeleteBuild(build.ToDomain());
            if (result == 0)
                return TypedResults.InternalServerError("Build deletion failed");
            return TypedResults.Ok();

        }
    }
}
