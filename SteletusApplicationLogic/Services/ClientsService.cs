using SteletusApplicationCore.Models;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.Services
{
    public class ClientsService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientsService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> CreateClient(Client client)
        {
            int result = await _clientRepository.CreateClient(new Client(Guid.NewGuid(), client.Name, client.Description, client.CurrentChangeRequest, client.LastChangeRequest, client.Builds));
            return result;
        }

        public async Task<int> DeleteClient(Guid clientId)
        {
            int result = await _clientRepository.DeleteClient(clientId);
            return result;
        }

        public async Task<Client?> GetClientById(Guid id)
        {
            Client? client = await _clientRepository.GetClientById(id);
            return client;
        }

        public async Task<IEnumerable<Client>> ListClients(Guid? buildId)
        {
            IEnumerable<Client> clients = await _clientRepository.ListClients(buildId);
            return clients;
        }

        public async Task<int> UpdateClient(Client client)
        {
            int result = await _clientRepository.UpdateClient(client);
            return result;
        }
    }
}
