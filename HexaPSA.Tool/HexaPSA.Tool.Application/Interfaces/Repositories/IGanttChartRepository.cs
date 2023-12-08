using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface IGanttChartReadRepository : IReadRepository<GanttChartResponse, Guid>
    {

    }

    public interface IGanttChartWriteRepository : IWriteRepository<GanttChartResponse, Guid>
    {

    }

    public interface IGanttChartRepository : IGanttChartReadRepository, IGanttChartWriteRepository
    {
    }
}