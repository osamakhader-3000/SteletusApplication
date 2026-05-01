using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Mappers
{
    public static class BuildMapper
    {
        public static SteletusApplicationCore.Models.Build ToCoreModel(this Models.Build build)
        {
            return new SteletusApplicationCore.Models.Build(build.Id, build.BuildVersion??string.Empty, build.Link, build.Clients?.ToCoreModelList(), build.BuildLogs?.ToCoreModels());
        }

        public static Models.Build ToDataModel(this SteletusApplicationCore.Models.Build build)
        {
            Models.Build bld = new() { Id = build.Id, BuildVersion = build.Version, Link = build.Link };

            List<Models.Client> buildClients = new List<Models.Client>();

            if (build.Clients != null)
            {

                foreach (SteletusApplicationCore.Models.Client client in build.Clients)
                {
                    Models.Client buildClient = new Models.Client
                    {
                        Id = client.Id,
                        CurrentChangeRequest = client.CurrentChangeRequest,
                        LastChangeRequest = client.LastChangeRequest,
                        Name = client.Name,
                        Description= client.Description
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

        public static IEnumerable<Models.Build> ToDataModelList(this IEnumerable<SteletusApplicationCore.Models.Build> builds)
        {
            return builds.Select(b => b.ToDataModel());
        }

    }
}
