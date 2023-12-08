using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IActivityLogReadRepository : IReadRepository<ActivityLog, Guid>
    {
        Task<IEnumerable<ActivityLog>> GetAllAsync();

    }

    public interface IActivityLogWriteRepository : IWriteRepository<ActivityLog, Guid>
    {

    }
    public interface IActivityLogRepository : IActivityLogReadRepository, IActivityLogWriteRepository
    {
        Task<IEnumerable<RecentExportAcivitiesResponse>> GetExportRecentAcivities();
        Task<IEnumerable<RecentAcivitiesResponse>> GetRecentAcivities();
    }
}
