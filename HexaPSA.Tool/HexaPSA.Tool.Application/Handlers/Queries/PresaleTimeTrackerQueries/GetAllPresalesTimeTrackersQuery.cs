using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using static GetAllPresalesTimeTrackersQueryHandler;

public sealed class GetAllPresalesTimeTrackersQueryHandler : IQueryHandler<GetAllPresalesTimeTrackersQuery, List<PresalesTimeTrackerResponse>>
{
    public sealed record GetAllPresalesTimeTrackersQuery : IQuery<List<PresalesTimeTrackerResponse>>;

    private readonly IPresalesTimeTrackerRepository _presalesTimeTrackerRepository;
    private readonly IMapper _mapper;

    public GetAllPresalesTimeTrackersQueryHandler(IPresalesTimeTrackerRepository presalesTimeTrackerRepository, IMapper mapper)
    {
        _presalesTimeTrackerRepository = presalesTimeTrackerRepository;
        _mapper = mapper;
    }

    public async Task<List<PresalesTimeTrackerResponse>> Handle(GetAllPresalesTimeTrackersQuery request, CancellationToken cancellationToken)
    {
        var presalesTimeTrackers = await _presalesTimeTrackerRepository.GetAllAsync();

        return _mapper.Map<List<PresalesTimeTrackerResponse>>(presalesTimeTrackers);
    }
}

