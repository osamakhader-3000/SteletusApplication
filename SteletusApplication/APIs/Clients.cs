using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationPresentation.Mappers;

namespace SteletusApplicationPresentation.APIs
{
    public static class Clients
    {
        public static void Map(RouteGroupBuilder apiGroup)
        {
            RouteGroupBuilder clientGroup = apiGroup.MapGroup("/clients").RequireAuthorization("VerifiedEmail", "ActiveAccount");
            clientGroup.MapGet("/list", ListClients);
            clientGroup.MapGet("/get/{id}", GetClient);
            clientGroup.MapPost("/create", CreateClient);
            clientGroup.MapPut("/update", UpdateClient);
            clientGroup.MapDelete("/delete/{id}", DeleteClient);
        }

        private static async Task<Ok<IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Client>>> ListClients(IClientService clientService, [FromQuery] Guid? buildId)
        {
            var clients = await clientService.ListClients(buildId);
            return TypedResults.Ok(clients.ToDtoList());
        }

        private static async Task<Results<Ok<SteletusApplicationPresentation.DataTransferObjects.Client>, NotFound>> GetClient(IClientService clientService, [FromRoute] Guid id)
        {
            var client = await clientService.GetClientById(id);
            if (client == null)
                return TypedResults.NotFound();
            return TypedResults.Ok(client.ToDto());
        }

        private static async Task<Results<Ok, InternalServerError<string>>> CreateClient(IClientService clientService, [FromBody] SteletusApplicationPresentation.DataTransferObjects.Client client)
        {
            int result = await clientService.CreateClient(client.ToDomain());
            if (result == 0)
                return TypedResults.InternalServerError("Failed to create client");
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, InternalServerError<string>>> UpdateClient(IClientService clientService, [FromBody] SteletusApplicationPresentation.DataTransferObjects.Client client)
        {
            await clientService.UpdateClient(client.ToDomain());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, NotFound>> DeleteClient(IClientService clientService, [FromRoute] Guid id)
        {
            int result = await clientService.DeleteClient(id);
            if (result == 0)
                return TypedResults.NotFound();
            return TypedResults.Ok();
        }

    }
}
