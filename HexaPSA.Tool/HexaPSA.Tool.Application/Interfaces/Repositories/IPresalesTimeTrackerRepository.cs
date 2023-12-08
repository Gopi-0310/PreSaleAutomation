using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IPresalesTimeTrackerReadRepository : IReadRepository<PresalesTimeTracker, Guid>
    {
       Task<PresalesTimeTracker> GetByNameAsync(string name);            
    }
    public interface IPresalesTimeTrackerWriteRepository : IWriteRepository<PresalesTimeTracker, Guid>
    {
        
    }
    public interface IPresalesTimeTrackerRepository : IPresalesTimeTrackerReadRepository, IPresalesTimeTrackerWriteRepository
    {
       
    }
}
