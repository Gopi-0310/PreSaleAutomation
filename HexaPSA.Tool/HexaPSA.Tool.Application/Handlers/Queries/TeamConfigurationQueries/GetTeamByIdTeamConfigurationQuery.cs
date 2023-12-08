using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Queries.TeamConfigurationQueries
{
    public sealed record GetTeamByIdTeamConfigurationQuery(Guid Id) : IQuery<TeamConfigResponse>;

    internal sealed class GetTeamByIdTeamConfigurationQueryHandler : IQueryHandler<GetTeamByIdTeamConfigurationQuery, TeamConfigResponse>
    {
        private readonly ITeamConfigurationRepository _teamConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTeamByIdTeamConfigurationQueryHandler(ITeamConfigurationRepository teamConfigurationRepository, IMapper mapper)
        {
            _teamConfigurationRepository = teamConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<TeamConfigResponse> Handle(GetTeamByIdTeamConfigurationQuery request, CancellationToken cancellationToken)
        {
            var teamConfiguration = await _teamConfigurationRepository.GetByIdAsync(request.Id);

            if (teamConfiguration is null)
            {

            }

            return _mapper.Map<TeamConfigResponse>(teamConfiguration);
        }
    }
}
