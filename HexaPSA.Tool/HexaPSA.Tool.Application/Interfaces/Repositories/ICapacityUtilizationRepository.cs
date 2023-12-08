using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface ICapacityUtilizationReadRepository : IReadRepository<CapacityUtilization, Guid>
    {
        Task<List<CapacityMappingResponse>> GetCapacityUtilizationByIdAsync(Guid id);
    }
    public interface ICapacityUtilizationWriteRepository : IWriteRepository<CapacityUtilization, Guid>
    {
    }
    public interface ICapacityUtilizationRepository : ICapacityUtilizationReadRepository, ICapacityUtilizationWriteRepository
    {
    }
}
