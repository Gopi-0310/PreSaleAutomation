using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IResourceRateCardReadRepository : IReadRepository<ResourceRate, Guid>
    {
        Task<ResourceRate> GetByIdAsync(Guid id);
        
    }

    public interface IResourceRateCardWriteRepository : IWriteRepository<ResourceRate, Guid>
    {
       
    }
    public interface IResourceRateCardRepository : IResourceRateCardReadRepository, IResourceRateCardWriteRepository
    {
       
    }
}
