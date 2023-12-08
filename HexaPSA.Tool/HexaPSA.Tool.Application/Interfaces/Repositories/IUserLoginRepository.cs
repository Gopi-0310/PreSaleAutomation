using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IUserLoginReadRepository : IReadRepository<UserLogin, Guid>
    {
        Task<UserLogin> GetByNameAsync(string email);
    }

    public interface IUserLoginWriteRepository : IWriteRepository<UserLogin, Guid>
    {

    }
        
    public interface IUserLoginRepository : IUserLoginReadRepository, IUserLoginWriteRepository
    {

    }
}
