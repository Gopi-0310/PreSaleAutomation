using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IRoleReadRepository : IReadRepository<Role, Guid>
    {
        Task<Role> GetByNameAsync(string name);
    }

    public interface IRoleWriteRepository : IWriteRepository<Role, Guid>
    {

    }

    public interface IRoleRepository : IRoleReadRepository, IRoleWriteRepository
    {

    }
}
