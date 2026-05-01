using Microsoft.EntityFrameworkCore;
using SteletusApplicationDataAccess.Mappers;
using SteletusApplicationDataAccess.Models;
using SteletusApplicationLogic.IRepositories;

namespace SteletusApplicationDataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Models.SteletusApplicationDataAccessContext _context;

        public ClientRepository(Models.SteletusApplicationDataAccessContext context)
        {
            _context = context;
        }

        public async Task<int> CreateClient(SteletusApplicationCore.Models.Client client)
        {
            var clnt = new Client
            {
                Id = client.Id,
                Name = client.Name,
                CurrentChangeRequest = client.CurrentChangeRequest,
                LastChangeRequest = client.LastChangeRequest,
                Description = client.Description,
                Builds = new List<Build>()
            };

            if (client.Builds != null && client.Builds.Any())
            {
                foreach (var build in client.Builds)
                {
                    var existingBuild = build.ToDataModel();
                    _context.Attach(existingBuild);
                    clnt.Builds.Add(existingBuild);
                }
            }

            _context.Clients.Add(clnt);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateClient(SteletusApplicationCore.Models.Client client)
        {
            var existingClient = await _context.Clients.Include(c => c.Builds).FirstOrDefaultAsync(c => c.Id == client.Id);

            if (existingClient == null)
                return 0;

            existingClient.Name = client.Name;
            existingClient.Description = client.Description;
            existingClient.CurrentChangeRequest = client.CurrentChangeRequest;
            existingClient.LastChangeRequest = client.LastChangeRequest;

            existingClient.Builds ??= [];
            existingClient.Builds.Clear();

            if (client.Builds != null)
            {
                var existingBuilds = await _context.Builds.Where(b => client.Builds.Select(cb => cb.Id).Contains(b.Id)).ToListAsync();

                foreach (var build in client.Builds)
                {
                    var existingBuild = existingBuilds.Find(b => b.Id == build.Id);
                    if (existingBuild != null)
                        existingClient.Builds.Add(existingBuild);
                }
            }

            return await _context.SaveChangesAsync();
        }


        public async Task<int> DeleteClient(Guid id)
        {
            Client? client = await _context.Clients.Include(c => c.Builds).FirstOrDefaultAsync(c => c.Id == id);

            if (client == null) 
                throw new KeyNotFoundException("Client with not found.");

            _context.Clients.Remove(client);

            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<SteletusApplicationCore.Models.Client?> GetClientById(Guid id)
        {
            Client? client = await _context.Clients
                .Where(c => c.Id == id)
                .Select(c => new Client
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CurrentChangeRequest = c.CurrentChangeRequest,
                    LastChangeRequest = c.LastChangeRequest,
                    Builds = c.Builds != null ? c.Builds.Select(b => new Build { Id = b.Id , BuildVersion = b.BuildVersion }).ToList() : null
                }).FirstOrDefaultAsync(c => c.Id == id);
                

            if (client == null)
                return null;

            return client.ToCoreModel();
        }

        public async Task<IEnumerable<SteletusApplicationCore.Models.Client>> ListClients(Guid? buildId)
        {
            IQueryable<Client> query = _context.Clients;

            if (buildId.HasValue)
            {
                query = _context.Clients.Where(c => c.Builds != null && c.Builds.Any(b => b.Id == buildId.Value));
            }

            var clients = await query.ToListAsync();
            return clients.Select(c => c.ToCoreModel());

        }


    }
}
