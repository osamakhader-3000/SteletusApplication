using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationPresentation.DataTransferObjects;
using SteletusApplicationPresentation.Mappers;

namespace SteletusApplicationPresentation.APIs
{
    public static class Builds
    {
        public static void Map(RouteGroupBuilder apiGroup)
        {
            RouteGroupBuilder buildGroup = apiGroup.MapGroup("/builds").RequireAuthorization("VerifiedEmail", "ActiveAccount","TechnicalSupportLevel");
            buildGroup.MapGet("/list", ListBuilds);
            buildGroup.MapGet("/get/{id}", GetBuild);
            buildGroup.MapPost("/create", CreateBuild);
            buildGroup.MapPut("/update", UpdateBuild);
            buildGroup.MapDelete("/delete/{id}", DeleteBuild);
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

        private static async Task<Results<Ok, InternalServerError<string>>> CreateBuild(IBuildService buildService, [FromBody] Build build)
        {
            int result = await buildService.CreateBuild(build.ToDomain());
            if (result == 0)
                return TypedResults.InternalServerError("Build creation failed");
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, InternalServerError<string>>> UpdateBuild(IBuildService buildService, [FromBody] Build build)
        {
            int result = await buildService.UpdateBuild(build.ToDomain());
            if (result == 0)
                return TypedResults.InternalServerError("Build update failed");
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, NotFound, InternalServerError<string>>> DeleteBuild(IBuildService buildService, [FromRoute] Guid id)
        {           
            int result = await buildService.DeleteBuild(id);
            if (result == 0)
                return TypedResults.InternalServerError("Build deletion failed");
            return TypedResults.Ok();

        }
    }
}
