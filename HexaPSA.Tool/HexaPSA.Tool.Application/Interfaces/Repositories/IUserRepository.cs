using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IUserReadRepository : IReadRepository<User, Guid>
    {
        Task<User> GetByNameAsync(string name);
        Task<IEnumerable<AllUserResponse>> GetAllAsync();
    }

    public interface IUserWriteRepository : IWriteRepository<User, Guid>
    {
        
    }

    public interface IUserRepository : IUserReadRepository, IUserWriteRepository
    {
        
    }
}
