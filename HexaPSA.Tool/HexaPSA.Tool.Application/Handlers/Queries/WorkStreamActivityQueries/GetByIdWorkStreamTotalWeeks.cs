using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries
{
    public sealed record GetByIdWorkStreamTotalWeeks(Guid Id) : IQuery<WorkStreamTotalResponse>;

    public sealed class GetByIdWorkStreamTotalWeeksQueryHandler : IRequestHandler<GetByIdWorkStreamTotalWeeks, WorkStreamTotalResponse>
    {
        private readonly IWorkStreamActivityRepository _workStreamActivityRepository;
        private readonly IWorkStreamRepository _workStreamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetByIdWorkStreamTotalWeeksQueryHandler(IWorkStreamActivityRepository workStreamActivityRepository, IMapper mapper, IProjectRepository projectRepository, IWorkStreamRepository workStream)
        {
            _workStreamActivityRepository = workStreamActivityRepository;
            _workStreamRepository = workStream;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<WorkStreamTotalResponse> Handle(GetByIdWorkStreamTotalWeeks request, CancellationToken cancellationToken)
        {
            var WorkStreamActivityList = await _workStreamActivityRepository.GetByIdAsync(request.Id);
            var projectDetail = await _projectRepository.GetByIdAsync(request.Id);
            var workStreamList = await _workStreamRepository.GetByIdAsync(request.Id);

            var totalHW = new WorkStreamTotalResponse();

            if (workStreamList != null && workStreamList.Any() && projectDetail != null)
            {
                foreach (var ws in workStreamList)
                {
                   /* var filteredActivities = WorkStreamActivityList
                        .Where(activity => activity.WorkStream.Id == ws.Id);*/
                    var filteredActivities = WorkStreamActivityList;
                    if (filteredActivities.Any())
                    {
                        var totalWeek = filteredActivities.Sum(activity => activity.Week); 
                        var totalHours = filteredActivities.Sum(activity => activity.Hours); 
                        var totalHourAndWeeks = new WorkStreamTotalResponse
                        {
                            ProjectId = ws.ProjectId,
                            Week = (int)totalWeek,
                            Hours = totalHours,
                        };

                        totalHW = totalHourAndWeeks;
                        return totalHW;
                    }
                }
            }

            return totalHW;
        }

    }


}
