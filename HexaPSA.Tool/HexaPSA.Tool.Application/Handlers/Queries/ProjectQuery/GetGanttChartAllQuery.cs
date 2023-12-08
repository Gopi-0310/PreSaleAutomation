using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Queries.ProjectQuery
{
    public sealed record GetGanttChartAllQuery : IQuery<List<GanttChartResponse>>;
    public sealed class GetGanttChartAllQueryHandler : IQueryHandler<GetGanttChartAllQuery, List<GanttChartResponse>>
    {
        private readonly IGanttChartRepository _ganttChartRepository;
        private readonly IMapper _mapper;

        public GetGanttChartAllQueryHandler(IGanttChartRepository ganttChartRepository, IMapper mapper)
        {
            _ganttChartRepository = ganttChartRepository;
            _mapper = mapper;
        }

        public async Task<List<GanttChartResponse>> Handle(GetGanttChartAllQuery request, CancellationToken cancellationToken)
        {
            var ganttChart = await _ganttChartRepository.GetAllAsync();

            return _mapper.Map<List<GanttChartResponse>>(ganttChart);
        }
    }
}
