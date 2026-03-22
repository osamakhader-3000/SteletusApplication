using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationPresentation.Mappers;
using System.Security.Claims;

namespace SteletusApplicationPresentation.APIs
{
    public static class Tasks
    {
        public static void Map(RouteGroupBuilder apiGroup)
        {
            RouteGroupBuilder taskGroup = apiGroup.MapGroup("/tasks").RequireAuthorization("VerifiedEmail", "ActiveAccount");
            taskGroup.MapGet("/list", ListUserTasks);
            taskGroup.MapGet("/get/{id}", GetTask);
            taskGroup.MapPost("/create", CreateTask);
            taskGroup.MapPut("/update", UpdateTask);

            RouteGroupBuilder adminTaskGroup = taskGroup.MapGroup("/admin").RequireAuthorization("AdminLevel");
            adminTaskGroup.MapGet("/list", ListTasks);
        }

        private static async Task<Results<Ok, UnauthorizedHttpResult, InternalServerError<string>>> CreateTask(HttpContext context, ITasksService tasksService, [FromBody] SteletusApplicationPresentation.DataTransferObjects.Task task)
        {
            string? userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.Unauthorized();
            }

            Guid userId = Guid.Parse(userIdClaim);

            task.AssignedTo = userId;
            task.AssignedBy = userId;

            int result = await tasksService.CreateTask(task.ToDomain());

            if (result == 0)
                return TypedResults.InternalServerError("Task creation failed");

            return TypedResults.Ok();
        }


        private static async Task<Results<Ok, UnauthorizedHttpResult, InternalServerError<string>>> UpdateTask(HttpContext context, ITasksService tasksService, [FromBody] SteletusApplicationPresentation.DataTransferObjects.Task task)
        {

            string? userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.Unauthorized();
            }

            Guid userId = Guid.Parse(userIdClaim);

            task.AssignedTo = userId;
            task.AssignedBy = userId;

            int result = await tasksService.UpdateTask(task.ToDomain());

            if (result == 0)
                return TypedResults.InternalServerError("Task update failed");

            return TypedResults.Ok();

        }



        private static async Task<Results<UnauthorizedHttpResult, NotFound, Ok<SteletusApplicationPresentation.DataTransferObjects.Task>>> GetTask(HttpContext context, ITasksService tasksService, Guid id)
        {
            string? userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return TypedResults.Unauthorized();
            }


            SteletusApplicationCore.Models.Task? task = await tasksService.GetTaskById(id);


            if (task == null)
                return TypedResults.NotFound();


            Guid userId = Guid.Parse(userIdClaim);

            if (task.AssignedTo != userId || task.AssignedBy != userId)
            {
                return TypedResults.Unauthorized();
            }


            return TypedResults.Ok<SteletusApplicationPresentation.DataTransferObjects.Task>(task.ToDto());

        }

        private static async Task<Results<UnauthorizedHttpResult, Ok<List<SteletusApplicationPresentation.DataTransferObjects.Task>>>> ListUserTasks(HttpContext context, ITasksService tasksService)
        {
            string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return TypedResults.Unauthorized();
            }

            List<SteletusApplicationCore.Models.Task> tasks = await tasksService.ListTasks(Guid.Parse(userId));


            return TypedResults.Ok<List<SteletusApplicationPresentation.DataTransferObjects.Task>>(tasks.ToDtoList().ToList());

        }

        public static async Task<Results<UnauthorizedHttpResult, Ok<List<SteletusApplicationPresentation.DataTransferObjects.Task>>>> ListTasks(ITasksService tasksService, [FromQuery] Guid? userId = null)
        {
            List<SteletusApplicationCore.Models.Task> tasks = await tasksService.ListTasks(userId);

            return TypedResults.Ok<List<SteletusApplicationPresentation.DataTransferObjects.Task>>(tasks.ToDtoList().ToList());
        }
    }
}
