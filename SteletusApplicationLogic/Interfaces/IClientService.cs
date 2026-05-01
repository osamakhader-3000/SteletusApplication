using SteletusApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> ListClients(Guid? buildId);

        Task<Client?> GetClientById(Guid id);

        Task<int> CreateClient(Client client);

        Task<int> DeleteClient(Guid clientId);

        Task<int> UpdateClient(Client client);

    }
}
