using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Queries.TeamConfigurationQueries
{
    public sealed record GetByIdTeamConfigurationQuery(Guid Id) : IQuery<List<TeamConfigurationResponse>>;
    internal sealed class GetByIdTeamConfigurationQueryHandler : IQueryHandler<GetByIdTeamConfigurationQuery, List<TeamConfigurationResponse>>
    {
        private readonly ITeamConfigurationRepository _teamConfigRepository;
        private readonly IMapper _mapper;

        public GetByIdTeamConfigurationQueryHandler(IMapper mapper, ITeamConfigurationRepository teamConfigRepository)
        {
            _teamConfigRepository = teamConfigRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamConfigurationResponse>> Handle(GetByIdTeamConfigurationQuery request, CancellationToken cancellationToken)
        {
            var teamConfiguration = await _teamConfigRepository.GetTeamConfigurationByIdAsync(request.Id);

            if (teamConfiguration is null)
            {

            }

            return _mapper.Map<List<TeamConfigurationResponse>>(teamConfiguration);
        }
    }
}
