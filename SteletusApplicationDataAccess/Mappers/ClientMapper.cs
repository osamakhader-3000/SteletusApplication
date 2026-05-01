using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Mappers
{
    public static class ClientMapper
    {
        public static SteletusApplicationCore.Models.Client ToCoreModel(this Models.Client client)
        {
            return new SteletusApplicationCore.Models.Client(client.Id, client.Name, client.Description, client.CurrentChangeRequest, client.LastChangeRequest, client.Builds?.ToCoreModelList());
        }

        public static Models.Client ToDataModel(this SteletusApplicationCore.Models.Client client)
        {
            return new Models.Client
            {
                Id = client.Id,
                Name = client.Name,
                Description = client.Description,
                CurrentChangeRequest = client.CurrentChangeRequest,
                LastChangeRequest = client.LastChangeRequest
            };
        }

        public static IEnumerable<SteletusApplicationCore.Models.Client> ToCoreModelList(this List<Models.Client> clients)
        {
            return clients.Select(c => c.ToCoreModel());
        }

        public static IEnumerable<Models.Client> ToDataModelList(this IEnumerable<SteletusApplicationCore.Models.Client> clients)
        {
            return clients.Select(c => c.ToDataModel());
        }

    }
}
