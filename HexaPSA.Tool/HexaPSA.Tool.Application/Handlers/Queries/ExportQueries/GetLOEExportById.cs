using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HexaPSA.Tool.Application.Handlers.Queries.ExportQueries
{
  
    public sealed record GetLOEExportById(Guid Id) : IQuery<ProjectResponse>;


    internal sealed class GetLOEExportQueryHandler : IQueryHandler<GetLOEExportById, ProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly ICapacityUtilizationRepository _rrRepository;
        private readonly IMapper _mapper;

        public GetLOEExportQueryHandler(IMapper mapper,
            IProjectRepository projectRepository,
            ICapacityUtilizationRepository rrRepository,
            IActivityLogRepository activityLogRepository)
        {
            _projectRepository = projectRepository;
            _rrRepository = rrRepository;
            _mapper = mapper;
            _activityLogRepository = activityLogRepository;
        }

        public async Task<ProjectResponse> Handle(GetLOEExportById request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            var resourceList = await _projectRepository.GetCostByResource(request.Id);
            var streamList = await _projectRepository.GetCostByWorkStreams(request.Id);
            var rateList = await _rrRepository.GetCapacityUtilizationByIdAsync(request.Id);
            var wsList = await _projectRepository.GetWorkStreamActivities(request.Id);
            var projectResponse = _mapper.Map<ProjectResponse>(project);

            projectResponse.CostSummary = new CostSummary();
            projectResponse.CostSummary.CostByWorkStreamList = streamList.ToList();
            projectResponse.CostSummary.CostByResourceList =  resourceList.ToList();
            projectResponse.ResourceRateList = GetResourceRateRespList(rateList);
            projectResponse.WorkStreamActivityResponses = wsList.ToList();
            projectResponse.CostSummary.Weeks = GetWeeks(wsList);

            projectResponse.WorkStreamResponses = GetWorkStreamResponses(wsList);

            ActivityLog activityLog = new() 
            { 
                ProjectId = project.Id,
                LogActivity =   "LOE",
                CreatedBy = project.CreatedBy,
            };
            await _activityLogRepository.AddAsync(activityLog);
            return  projectResponse;
        }

        private List<ResourceRateResponseExport> GetResourceRateRespList(List<CapacityMappingResponse> capacity)
        {
            var rrList = new List<ResourceRateResponseExport>();
            foreach (var capacityMappingResponse in capacity) {
                rrList.Add(new ResourceRateResponseExport()
                {
                    Code = capacityMappingResponse.Role.Code,
                    Role = capacityMappingResponse.Role.Name,
                    Location ="OnSite",
                    Rate = capacityMappingResponse.ResourceRate.Rate
                }); 
            }
            return rrList;
        }

        private List<int> GetWeeks(IEnumerable<WorkStreamActivityResp> resp)
        {
            var weekList = resp.Where(i => i.Week != null && i.Week > 0).Select(item => Convert.ToInt32(item.Week)).Distinct();
            return weekList.OrderBy(p => p).ToList();
        }
        private List<WorkStreamResp> GetWorkStreamResponses(IEnumerable<WorkStreamActivityResp> resp)
        {
            var wsList = resp.Where(i => i.Depth == 0);

            var wsDict = new Dictionary<Guid, string>();
            foreach (var ws in wsList)
            {
                if (!wsDict.ContainsKey(ws.WorkStreamActivityId))
                    wsDict[ws.WorkStreamActivityId] = ws.Name;
            }

            var wsRespList=  new List<WorkStreamResp>();
            WorkStreamResp workStreamResp = null;
            foreach (var ws in wsDict)
            {
                workStreamResp = new WorkStreamResp
                {
                    Id = ws.Key,
                    Name = ws.Value,
                    WorkStreamActivityList = resp.Where(item => item.WorkStreamActivityId == ws.Key).ToList()
                };
                wsRespList.Add(workStreamResp);
            }
            return wsRespList;
        }
    }

}


