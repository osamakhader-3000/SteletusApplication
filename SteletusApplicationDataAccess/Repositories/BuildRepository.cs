using Microsoft.EntityFrameworkCore;
using SteletusApplicationDataAccess.Mappers;
using SteletusApplicationDataAccess.Models;
using SteletusApplicationLogic.IRepositories;

namespace SteletusApplicationDataAccess.Repositories
{
    public class BuildRepository : IBuildRepository
    {

        private readonly Models.SteletusApplicationDataAccessContext _context;

        public BuildRepository(Models.SteletusApplicationDataAccessContext context)
        {
            _context = context;
        }


        public async Task<int> CreateBuild(SteletusApplicationCore.Models.Build build)
        {
            var bld = new Models.Build
            {
                Id = build.Id,
                BuildVersion = build.Version,
                Link = build.Link,
                Clients = new List<Models.Client>()
            };

            if (build.Clients != null && build.Clients.Any())
            {
                foreach (var client in build.Clients)
                {
                    var existingClient = new Models.Client { Id = client.Id };
                    _context.Attach(existingClient);
                    bld.Clients.Add(existingClient);
                }
            }

            if (build.BuildLogs != null && build.BuildLogs.Any())
            {
                foreach (var log in build.BuildLogs)
                {
                    var existingLog = log.ToDataModel();
                    _context.BuildLogs.Add(existingLog);
                }
            }

            _context.Builds.Add(bld);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateBuild(SteletusApplicationCore.Models.Build build)
        {
            var dbBld = await _context.Builds.Include(b => b.Clients).FirstAsync(b => b.Id == build.Id);

            dbBld.Link = build.Link;
            dbBld.BuildVersion = build.Version;


            if (build.Clients?.Any() == true)
            {
                dbBld.Clients ??= [];
                dbBld.Clients.Clear();

                List<Client> existingClients = await _context.Clients.Where(c => build.Clients.Select(cl => cl.Id).Contains(c.Id)).ToListAsync();


                foreach (var client in build.Clients)
                {
                    Client? existingClient = existingClients.FirstOrDefault(c => c.Id == client.Id);
                    if(existingClient != null)
                        dbBld.Clients.Add(existingClient);
                }

            }

            if (build.BuildLogs != null && build.BuildLogs.Any())
            {

                foreach (var log in build.BuildLogs)
                {
                    var existingLog = log.ToDataModel();
                    _context.BuildLogs.Add(existingLog);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBuild(SteletusApplicationCore.Models.Build build)
        {
            var existingLog = build.BuildLogs?.FirstOrDefault()?.ToDataModel();

            var dbBuild = await _context.Builds.Include(b => b.Clients).FirstOrDefaultAsync(b => b.Id == build.Id);

            if (dbBuild == null)
                return 0;

            dbBuild.Clients ??= [];
            dbBuild.Clients.Clear();

            if (existingLog != null)
                _context.BuildLogs.Add(existingLog);

            _context.Builds.Remove(dbBuild);

            return await _context.SaveChangesAsync();
        }

        public async Task<SteletusApplicationCore.Models.Build?> GetBuildById(Guid buildId)
        {
            var build = await _context.Builds
                .Where(b => b.Id == buildId)
                .Select(b => new Models.Build
                {
                    Id = b.Id,
                    BuildVersion=b.BuildVersion,
                    Link=b.Link,
                    BuildLogs = b.BuildLogs == null ? null : b.BuildLogs.Select(l => new BuildLog { Id = l.Id , BuildId = l.BuildId, Timestamp = l.Timestamp, UserId = l.UserId }).ToList(),
                    Clients = b.Clients == null ? null : b.Clients.Select(c => new Client{Id = c.Id , Name = c.Name}).ToList()
                }).FirstOrDefaultAsync(); 
                

            if (build == null)
                return null;

            return build.ToCoreModel();
        }

        public async Task<IEnumerable<SteletusApplicationCore.Models.Build>> ListBuilds()
        {
            List<Models.Build> builds = await _context.Builds.Include(b => b.BuildLogs).ToListAsync();
            return builds.Select(b => b.ToCoreModel());
        }

    }
}
