using SteletusApplicationCore.Models;

namespace SteletusApplicationLogic.IRepositories
{
    public interface IBuildRepository
    {
        Task<IEnumerable<Build>> ListBuilds();
        Task<Build?> GetBuildById(Guid buildId);
        Task<int> DeleteBuild(Build build);
        Task<int> CreateBuild(Build build);
        Task<int> UpdateBuild(Build build);
    }
}
