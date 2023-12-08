using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.ResourceRateQueries
{
    public sealed record GetAllResourceRateCardQuery : IQuery<List<ResourceRateCardResponse>>;
    internal sealed class GetResourceRateCardQueryHandler : IQueryHandler<GetAllResourceRateCardQuery, List<ResourceRateCardResponse>>
    {
        private readonly IResourceRateCardRepository _rateCardRepository;
        private readonly IMapper _mapper;

        public GetResourceRateCardQueryHandler(IResourceRateCardRepository rateCardRepository, IMapper mapper)
        {
            _rateCardRepository = rateCardRepository;
            _mapper = mapper;
        }

        public async Task<List<ResourceRateCardResponse>> Handle(GetAllResourceRateCardQuery request, CancellationToken cancellationToken)
        {
            var rateCard = await _rateCardRepository.GetAllAsync();

            return _mapper.Map<List<ResourceRateCardResponse>>(rateCard);
        }
    }
}
