using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IChartReadRepository : IReadRepository<ChartResponse, Guid>
    {
        
    }

    public interface IChartWriteRepository : IWriteRepository<ChartResponse, Guid>
    {
    
    }

    public interface IChartRepository : IChartReadRepository, IChartWriteRepository
    {
       
    }
}
