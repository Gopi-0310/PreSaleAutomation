using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.PresaleTimeTrackerQueries
{
    public sealed record GetPresalesTimeTrackerByIdQuery(Guid Id) : IQuery<PresalesTimeTrackerResponse>;
    internal sealed class GetPresalesTimeTrackerByIdQueryHandler : IQueryHandler<GetPresalesTimeTrackerByIdQuery, PresalesTimeTrackerResponse>
    {
        private readonly IPresalesTimeTrackerRepository _presalesTimeTrackerRepository;
        private readonly IMapper _mapper;

        public GetPresalesTimeTrackerByIdQueryHandler(IPresalesTimeTrackerRepository presalesTimeTrackerRepository, IMapper mapper)
        {
            _presalesTimeTrackerRepository = presalesTimeTrackerRepository;
            _mapper = mapper;
        }

        public async Task<PresalesTimeTrackerResponse> Handle(GetPresalesTimeTrackerByIdQuery request, CancellationToken cancellationToken)
        {
            var presalesTimeTracker = await _presalesTimeTrackerRepository.GetByIdAsync(request.Id);

            if (presalesTimeTracker is null)
            {
                // You can throw an exception if the PresalesTimeTracker is not found.
                // For example: throw new PresalesTimeTrackerNotFoundException(request.Id);
            }

            return _mapper.Map<PresalesTimeTrackerResponse>(presalesTimeTracker);
        }
    }

}
