using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Mappers
{
    public static class BuildMapper
    {
        public static SteletusApplicationCore.Models.Build ToCoreModel(this Models.Build build)
        {
            return new SteletusApplicationCore.Models.Build(build.Id, build.BuildVersion??string.Empty, build.Link, build.Clients?.Select(c=>c.ClientName).ToList());
        }

        public static Models.Build ToDataModel(this SteletusApplicationCore.Models.Build build)
        {
            Models.Build bld = new() { Id = build.Id, BuildVersion = build.Version, Link = build.Link };

            List<Models.BuildClient> buildClients = new List<Models.BuildClient>();

            if (build.Clients != null)
            {

                foreach (string clientName in build.Clients)
                {
                    Models.BuildClient buildClient = new Models.BuildClient
                    {
                        BuildId = build.Id,
                        ClientName = clientName,
                        Build = bld
                    };
                    buildClients.Add(buildClient);
                }

                bld.Clients = buildClients;
            }

            return bld;
        }

        public static IEnumerable<SteletusApplicationCore.Models.Build> ToCoreModelList(this List<Models.Build> builds)
        {
            return builds.Select(b => b.ToCoreModel());
        }

        public static IEnumerable<Models.Build> ToDataModelList(this IEnumerable<SteletusApplicationCore.Models.Build> buildDtos)
        {
            return buildDtos.Select(b => b.ToDataModel());
        }

    }
}
