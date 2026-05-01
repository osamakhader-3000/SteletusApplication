using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.IRepositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<SteletusApplicationCore.Models.Client>> ListClients(Guid? buildId);
        Task<SteletusApplicationCore.Models.Client?> GetClientById(Guid id);

        Task<int> CreateClient(SteletusApplicationCore.Models.Client client);

        Task<int> UpdateClient(SteletusApplicationCore.Models.Client client);

        Task<int> DeleteClient(Guid id);


    }
}
