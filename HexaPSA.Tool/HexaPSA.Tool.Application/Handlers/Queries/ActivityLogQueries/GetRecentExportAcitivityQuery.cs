using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.ActivityLogQueries
{
    public sealed record GetRecentExportAcitivityQuery() : IQuery<List<RecentExportAcivitiesResponse>>;

    internal sealed class GetRecentExportAcitivityHandler : IQueryHandler<GetRecentExportAcitivityQuery, List<RecentExportAcivitiesResponse>>
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IMapper _mapper;

        public GetRecentExportAcitivityHandler(IMapper mapper, IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
            _mapper = mapper;
        }

        public async Task<List<RecentExportAcivitiesResponse>> Handle(GetRecentExportAcitivityQuery request, CancellationToken cancellationToken)
        {
            var recentAcivities = await _activityLogRepository.GetExportRecentAcivities();

            return _mapper.Map<List<RecentExportAcivitiesResponse>>(recentAcivities);
        }
    }
}
