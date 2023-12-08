using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IWorkStreamReadRepository : IReadRepository<WorkStream, Guid>
    {
        Task<List<WorkStreamResponse>> GetByIdAsync(Guid id);
        Task<WorkStreamResponse> GetByIdDataAsync(Guid id);

    }

    public interface IWorkStreamWriteRepository : IWriteRepository<WorkStream, Guid>
    {
      
    }
    public interface IWorkStreamRepository : IWorkStreamReadRepository, IWorkStreamWriteRepository
    {
       
    }
}
