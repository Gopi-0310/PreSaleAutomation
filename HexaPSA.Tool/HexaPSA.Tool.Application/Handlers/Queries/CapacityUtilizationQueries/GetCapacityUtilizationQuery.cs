
using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;


namespace HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries
{
    public sealed record GetCapacityUtilizationQuery() : IQuery<List<CapacityMappingResponse>>;


    internal sealed class GetCapacityUtilizationQueryHandler : IQueryHandler<GetCapacityUtilizationQuery, List<CapacityMappingResponse>>
    {
        private readonly ICapacityUtilizationRepository _CapacityRepository;
        private readonly IMapper _mapper;

        public GetCapacityUtilizationQueryHandler(IMapper mapper, ICapacityUtilizationRepository capacityRepository)
        {
            _CapacityRepository = capacityRepository;
            _mapper = mapper;
        }

        public async Task<List<CapacityMappingResponse>> Handle(GetCapacityUtilizationQuery request, CancellationToken cancellationToken)
        {
            var capacityUtilization = await _CapacityRepository.GetAllAsync();

            return _mapper.Map<List<CapacityMappingResponse>>(capacityUtilization);
        }
    }

}


