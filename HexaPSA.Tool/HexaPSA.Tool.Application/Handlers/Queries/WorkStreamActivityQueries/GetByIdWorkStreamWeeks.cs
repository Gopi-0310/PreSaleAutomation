using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries
{

    public sealed record GetByIdWorkStreamWeeks(Guid Id) : IQuery<List<WorkStreamChartDetails>>;

    public sealed class GetByIdWorkStreamWeeksQueryHandler : IRequestHandler<GetByIdWorkStreamWeeks, List<WorkStreamChartDetails>>
    {
        private readonly IWorkStreamActivityRepository _workStreamActivityRepository;
        private readonly IWorkStreamRepository _workStreamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetByIdWorkStreamWeeksQueryHandler(IWorkStreamActivityRepository workStreamActivityRepository, IMapper mapper, IProjectRepository projectRepository, IWorkStreamRepository workStream)
        {
            _workStreamActivityRepository = workStreamActivityRepository;
            _workStreamRepository         = workStream;
            _projectRepository            = projectRepository;
            _mapper                       = mapper;
        }

        public async Task<List<WorkStreamChartDetails>> Handle(GetByIdWorkStreamWeeks request, CancellationToken cancellationToken)
        {
            var WorkStreamActivityList  = await _workStreamActivityRepository.GetByIdAsync(request.Id);
            var projectDetail           = await _projectRepository.GetByIdAsync(request.Id);
            var workStreamList          = await _workStreamRepository.GetByIdAsync(request.Id);

            var chartList = new List<WorkStreamChartDetails>();

            if (workStreamList != null && workStreamList.Any() && projectDetail != null)
            {
                foreach (var ws in workStreamList)
                {
                    var filteredActivities = WorkStreamActivityList
                        .Where(activity  => activity.WorkStream.Id == ws.Id)
                        .Select(activity => activity.Week);

                    if (filteredActivities.Any())
                    {
                        var minWeekForWorkStream = filteredActivities.Min();
                        var maxWeekForWorkStream = filteredActivities.Max();

                        var startDate = projectDetail.EffectiveStartDate.AddDays((minWeekForWorkStream - 1) * 7);
                        var endDate   = projectDetail.EffectiveStartDate.AddDays(maxWeekForWorkStream * 7);

                        var chartDetails = new WorkStreamChartDetails
                        {
                            Id        = ws.Id,
                            Name      = ws.Name,
                            StartDate = startDate,
                            EndDate   = endDate,
                        };

                        chartList.Add(chartDetails);
                    }
                }
            }

            return chartList;
        }







    }
}
