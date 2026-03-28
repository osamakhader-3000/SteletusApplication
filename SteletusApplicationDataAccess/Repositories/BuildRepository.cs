using Microsoft.EntityFrameworkCore;
using SteletusApplicationCore.Models;
using SteletusApplicationDataAccess.Mappers;
using SteletusApplicationLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

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
            Models.Build bld = build.ToDataModel();

            _context.Builds.Add(bld);

            if (bld.Clients != null && bld.Clients.Count > 0)
                _context.BuildClients.AddRange(bld.Clients);

            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteBuild(Guid buildId)
        {
            _context.BuildClients.RemoveRange(_context.BuildClients.Where(bc => bc.BuildId == buildId));
            _context.Builds.RemoveRange(_context.Builds.Where(b => b.Id == buildId));
            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<SteletusApplicationCore.Models.Build?> GetBuildById(Guid buildId)
        {
            Models.Build? build = await _context.Builds.Include(b => b.Clients).FirstOrDefaultAsync(b => b.Id == buildId);
            if (build == null)
                return null;
            return build.ToCoreModel();
        }

        public async Task<IEnumerable<SteletusApplicationCore.Models.Build>> ListBuilds()
        {
            List<Models.Build> builds = await _context.Builds.Include(b => b.Clients).ToListAsync();
            return builds.Select(b => b.ToCoreModel());
        }

        public async Task<int> UpdateBuild(SteletusApplicationCore.Models.Build build)
        {
            var bld = build.ToDataModel();

            var dbBld = await _context.Builds.Include(b => b.Clients).FirstAsync(b => b.Id == build.Id);

            dbBld.Link = build.Link;
            dbBld.BuildVersion = build.Version;

            if(dbBld.Clients == null)
                dbBld.Clients = new List<Models.BuildClient>();
            else
                dbBld.Clients.Clear();

            if (bld.Clients != null)
            {
                foreach (var client in bld.Clients)
                    dbBld.Clients.Add(client);
            }

            return await _context.SaveChangesAsync();
        }

    }
}
