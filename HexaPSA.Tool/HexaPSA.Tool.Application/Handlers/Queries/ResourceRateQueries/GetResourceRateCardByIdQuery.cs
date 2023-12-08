using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.ResourceRateQueries
{
    public sealed record GetResourceRateCardByIdQuery(Guid Id) : IQuery<ResourceRateCardResponse>;
    public sealed class GetResourceRateCardByIdQueryHandler : IQueryHandler<GetResourceRateCardByIdQuery, ResourceRateCardResponse>
    {
        private readonly IResourceRateCardRepository _rateCardRepository;
        private readonly IMapper _mapper;

        public GetResourceRateCardByIdQueryHandler(IResourceRateCardRepository reteCardRepository, IMapper mapper)
        {
            _rateCardRepository = reteCardRepository;
            _mapper = mapper;
        }

        public async Task<ResourceRateCardResponse> Handle(GetResourceRateCardByIdQuery request, CancellationToken cancellationToken)
        {
            var rateCard = await _rateCardRepository.GetByIdAsync(request.Id);

            if (rateCard is null)
            {
                //throw new RoleNotFoundException(request.Id);
            }

            return _mapper.Map<ResourceRateCardResponse>(rateCard);
        }
    }
}
