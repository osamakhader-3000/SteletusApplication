using SteletusApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.Interfaces
{
    public interface IBuildService
    {
        Task<IEnumerable<Build>> ListBuilds();

        Task<Build?> GetBuildById(Guid buildId);

        Task<int> DeleteBuild(Guid buildId);

        Task<int> CreateBuild(Build build);

        Task<int> UpdateBuild(Build build);

    }
}
