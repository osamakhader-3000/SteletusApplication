namespace SteletusApplicationPresentation.Mappers
{
    public static class ClientMapper
    {
        public static SteletusApplicationPresentation.DataTransferObjects.Client ToDto(this SteletusApplicationCore.Models.Client client)
        {
            return new SteletusApplicationPresentation.DataTransferObjects.Client
            {
                Id = client.Id,
                Name = client.Name,
                CurrentChangeRequest = client.CurrentChangeRequest,
                LastChangeRequest = client.LastChangeRequest,
                Description= client.Description,
                Builds = client.Builds?.Select(b => b.ToDto()).ToList()
            };
        }

        public static SteletusApplicationCore.Models.Client ToDomain(this SteletusApplicationPresentation.DataTransferObjects.Client client)
        {
            return new SteletusApplicationCore.Models.Client(client.Id ?? Guid.Empty, client.Name ?? string.Empty, client.Description, client.CurrentChangeRequest, client.LastChangeRequest, client.Builds?.Select(b => b.ToDomain()).ToList());
        }

        public static IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Client> ToDtoList(this IEnumerable<SteletusApplicationCore.Models.Client> clients)
        {
            return clients.Select(c => c.ToDto());
        }

        public static IEnumerable<SteletusApplicationCore.Models.Client> ToDomainList(this IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Client> clients)
        {
            return clients.Select(c => c.ToDomain());
        }

    }
}
