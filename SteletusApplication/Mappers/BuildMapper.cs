namespace SteletusApplicationPresentation.Mappers
{
    public static class BuildMapper
    {
        public static SteletusApplicationPresentation.DataTransferObjects.Build ToDto(this SteletusApplicationCore.Models.Build build)
        {
            return new SteletusApplicationPresentation.DataTransferObjects.Build
            {
                Id = build.Id,
                Link = build.Link,
                Version = build.Version,
                UpdatedAt = build.BuildLogs?.OrderByDescending(bl => bl.Timestamp).FirstOrDefault()?.Timestamp,
                UpdatedBy= build.BuildLogs?.OrderByDescending(bl => bl.Timestamp).FirstOrDefault()?.UserId,
                Clients = build.Clients?.ToDtoList().ToList()
            };
        }

        public static SteletusApplicationCore.Models.Build ToDomain(this SteletusApplicationPresentation.DataTransferObjects.Build build)
        {
            SteletusApplicationCore.Models.BuildLog buildLog = new(0, build.Id ?? Guid.Empty, DateTime.MinValue, build.UpdatedBy ?? Guid.Empty);
            return new SteletusApplicationCore.Models.Build(build.Id??Guid.Empty, build.Version ?? string.Empty, build.Link, build.Clients?.ToDomainList(), [buildLog]);
        }

        public static IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Build> ToDtoList(this IEnumerable<SteletusApplicationCore.Models.Build> builds)
        {
            return builds.Select(b => b.ToDto());
        }

        public static IEnumerable<SteletusApplicationCore.Models.Build> ToDomainList(this IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Build> builds)
        {
            return builds.Select(b => b.ToDomain());
        }

    }
}
