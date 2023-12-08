using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.ActivityLogQueries
{
    public sealed record GetRecentAcitivityQuery() : IQuery<List<RecentAcivitiesResponse>>;

    internal sealed class GetRecentAcitivityQueryHandler : IQueryHandler<GetRecentAcitivityQuery, List<RecentAcivitiesResponse>>
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IMapper _mapper;

        public GetRecentAcitivityQueryHandler(IMapper mapper, IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
            _mapper = mapper;
        }

        public async Task<List<RecentAcivitiesResponse>> Handle(GetRecentAcitivityQuery request, CancellationToken cancellationToken)
        {
            var recentAcivities = await _activityLogRepository.GetRecentAcivities();

            return _mapper.Map<List<RecentAcivitiesResponse>>(recentAcivities);
        }
    }

}
