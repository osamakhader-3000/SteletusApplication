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

        Task<int> DeleteBuild(Build build);

        Task<int> CreateBuild(Build build);

        System.Threading.Tasks.Task UpdateBuild(Build build);

    }
}
