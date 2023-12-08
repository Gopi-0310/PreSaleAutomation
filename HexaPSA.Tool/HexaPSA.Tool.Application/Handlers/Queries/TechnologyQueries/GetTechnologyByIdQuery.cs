using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts.Technology;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.TechnologyQueries
{
    public sealed record GetTechnologyByIdQuery(Guid Id) : IQuery<TechnologyDropResponse>;
    public sealed class GetTechnologyByIdQueryHandler : IQueryHandler<GetTechnologyByIdQuery, TechnologyDropResponse>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;

        public GetTechnologyByIdQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }

        public async Task<TechnologyDropResponse> Handle(GetTechnologyByIdQuery request, CancellationToken cancellationToken)
        {
            var tech = await _technologyRepository.GetByIdAsync(request.Id);

            if (tech is null)
            {
                //throw new RoleNotFoundException(request.Id);
            }

            return _mapper.Map<TechnologyDropResponse>(tech);
        }
    }
}
