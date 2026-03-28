namespace SteletusApplicationPresentation.Mappers
{
    public static class BuildMapper
    {
        public static SteletusApplicationPresentation.DataTransferObjects.Build ToDto(this SteletusApplicationCore.Models.Build build)
        {
            return new SteletusApplicationPresentation.DataTransferObjects.Build
            {
                Id = build.Id,
                Clients = build.Clients,
                Link = build.Link,
                Version = build.Version,
            };
        }

        public static SteletusApplicationCore.Models.Build ToDomain(this SteletusApplicationPresentation.DataTransferObjects.Build buildDto)
        {
            return new SteletusApplicationCore.Models.Build(buildDto.Id??Guid.Empty, buildDto.Version ?? string.Empty, buildDto.Link, buildDto.Clients);
        }

        public static IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Build> ToDtoList(this IEnumerable<SteletusApplicationCore.Models.Build> builds)
        {
            return builds.Select(b => b.ToDto());
        }

        public static IEnumerable<SteletusApplicationCore.Models.Build> ToDomainList(this IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Build> buildDtos)
        {
            return buildDtos.Select(b => b.ToDomain());
        }

    }
}
