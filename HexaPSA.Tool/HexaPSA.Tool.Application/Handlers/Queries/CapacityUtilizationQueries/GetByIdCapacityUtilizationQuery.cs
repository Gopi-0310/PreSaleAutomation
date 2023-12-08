using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Exceptions;

namespace HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries
{
    public sealed record GetByIdCapacityUtilizationQuery(Guid Id) : IQuery<List<CapacityMappingResponse>>;


    internal sealed class GetByIdCapacityUtilizationQueryHandler : IQueryHandler<GetByIdCapacityUtilizationQuery,List<CapacityMappingResponse>>
    {
        private readonly ICapacityUtilizationRepository _capacityRepository;
        private readonly IMapper _mapper;

        public GetByIdCapacityUtilizationQueryHandler(IMapper mapper, ICapacityUtilizationRepository capacityRepository)
        {
            _capacityRepository = capacityRepository;
            _mapper = mapper;
        }

        public async Task<List<CapacityMappingResponse>> Handle(GetByIdCapacityUtilizationQuery request, CancellationToken cancellationToken)
        {
            var capacityUtilization = await _capacityRepository.GetCapacityUtilizationByIdAsync(request.Id);

            if (capacityUtilization is null)
            {
                throw new ProjectTypeNotFoundException(request.Id);
            }

            return _mapper.Map<List<CapacityMappingResponse>>(capacityUtilization);
        }
    }
}
