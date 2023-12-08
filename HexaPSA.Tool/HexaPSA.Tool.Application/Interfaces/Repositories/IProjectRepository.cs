using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Contracts.Project;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IProjectReadRepository : IReadRepository<Project,Guid>
    {
        Task<ProjectResponse> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<ProjectResponseAll>> GetAllAsync();
        Task<IEnumerable<int>> WeeksByProject(Guid projectId);
        Task<IEnumerable<WorkStreamActivityResp>> GetWorkStreamActivities(Guid projectId);
        Task<IEnumerable<CostByResourceResp>> GetCostByResource(Guid projectId);

        Task<IEnumerable<CostByWorkStream>> GetCostByWorkStreams(Guid projectId);


    }
    public interface IProjectWriteRepository : IWriteRepository<Project, Guid>
    {
        Task<int> SaveAsync(Project item);
    }

    public interface IProjectRepository : IProjectReadRepository, IProjectWriteRepository
    {

    }
}
