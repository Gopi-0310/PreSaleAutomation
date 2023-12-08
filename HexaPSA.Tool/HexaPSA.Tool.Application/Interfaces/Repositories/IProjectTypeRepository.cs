using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{

    public interface IProjectTypeReadRepository : IReadRepository<ProjectType,Guid>
    {
        Task<ProjectType> GetByNameAsync(string name);
    }
    public interface IProjectTypeWriteRepository : IWriteRepository<ProjectType, Guid>
    {

    }

    public interface IProjectTypeRepository : IProjectTypeReadRepository, IProjectTypeWriteRepository
    {

    }
}
