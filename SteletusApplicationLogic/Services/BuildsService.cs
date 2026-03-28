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
            Build bld = new Build(Guid.NewGuid(), build.Version ?? string.Empty, build.Link, build.Clients);
            int result = await _buildRepository.CreateBuild(bld);
            return result;
        }

        public async Task<int> DeleteBuild(Guid buildId)
        {
            int result = await _buildRepository.DeleteBuild(buildId);
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

        public async Task<int> UpdateBuild(Build build)
        {
            int result = await _buildRepository.UpdateBuild(build);
            return result;
        }
    }
}
