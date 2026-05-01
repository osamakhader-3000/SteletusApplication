using SteletusApplicationCore.Models;
using SteletusApplicationLogic.Interfaces;
using SteletusApplicationLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.Services
{

    public class BuildsService : IBuildService
    {
        private readonly IBuildRepository _buildRepository;

        public BuildsService(IBuildRepository buildRepository)
        {
            _buildRepository = buildRepository;

        }

        public async Task<int> CreateBuild(Build build)
        {
            Guid buildId = Guid.NewGuid();
            BuildLog buildLog = new(0, buildId, DateTime.UtcNow, build.BuildLogs?.First().UserId ?? Guid.Empty);
            Build bld = new(buildId, build.Version ?? string.Empty, build.Link, build.Clients,new List<BuildLog> { buildLog });
            int result = await _buildRepository.CreateBuild(bld);
            return result;
        }

        public async System.Threading.Tasks.Task UpdateBuild(Build build)
        {
            BuildLog buildLog = new(0, build.Id, DateTime.UtcNow, build.BuildLogs?.First().UserId ?? Guid.Empty);
            Build bld = new(build.Id, build.Version ?? string.Empty, build.Link, build.Clients, new List<BuildLog> { buildLog });
            await _buildRepository.UpdateBuild(bld);
        }


        public async Task<int> DeleteBuild(Build build)
        {
            BuildLog buildLog = new(0, build.Id, DateTime.UtcNow, build.BuildLogs?.First().UserId ?? Guid.Empty);
            Build bld = new(build.Id, string.Empty, string.Empty, [], new List<BuildLog> { buildLog });
            int result = await _buildRepository.DeleteBuild(bld);
            return result;
        }

        public async Task<Build?> GetBuildById(Guid buildId)
        {
            Build? result = await _buildRepository.GetBuildById(buildId);
            return result;
        }

        public async Task<IEnumerable<Build>> ListBuilds()
        {
            IEnumerable<Build> result = await _buildRepository.ListBuilds();
            return result;
        }

    }
}
