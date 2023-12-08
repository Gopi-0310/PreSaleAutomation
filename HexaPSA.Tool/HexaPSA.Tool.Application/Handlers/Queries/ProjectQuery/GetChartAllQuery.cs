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
    public sealed record GetChartAllQuery : IQuery<List<ChartResponse>>;
    internal sealed class GetChartAllQueryHandler : IQueryHandler<GetChartAllQuery, List<ChartResponse>>
    {
        private readonly IChartRepository _chartRepository;
        private readonly IMapper _mapper;

        public GetChartAllQueryHandler(IChartRepository chartRepository, IMapper mapper)
        {
            _chartRepository = chartRepository;
            _mapper = mapper;
        }

        public async Task<List<ChartResponse>> Handle(GetChartAllQuery request, CancellationToken cancellationToken)
        {
            var chart = await _chartRepository.GetAllAsync();

            return _mapper.Map<List<ChartResponse>>(chart);
        }
    }

}
