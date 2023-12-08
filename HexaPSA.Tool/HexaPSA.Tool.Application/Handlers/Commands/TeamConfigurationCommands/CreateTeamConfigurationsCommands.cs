using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Exceptions.Project;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.TeamConfigurationCommands
{
    public sealed record CreateTeamConfigurationsCommand(Guid? UserId = null, Guid? ProjectId = null, Guid? RoleId = null) : ICommand<TeamConfigResponse>;
    public sealed record CreateTeamConfigurationsRequest(Guid? UserId = null, Guid? ProjectId = null, Guid? RoleId = null);

    public class CreateTeamConfigurationsHandler : ICommandHandler<CreateTeamConfigurationsCommand, TeamConfigResponse>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ITeamConfigurationRepository _teamConfigRepository;


        public CreateTeamConfigurationsHandler(IMapper mapper, ISender sender, ITeamConfigurationRepository teamConfigRepository)
        {
            _sender = sender;
            _mapper = mapper;
            _teamConfigRepository = teamConfigRepository;
 
        }

        public async Task<TeamConfigResponse> Handle(CreateTeamConfigurationsCommand command, CancellationToken cancellationToken)
        {
            TeamConfigResponse teamcon = new();

            // Map command to entity For New Create
            var item = _mapper.Map<TeamConfigResponse>(command);
            //item.CreatedBy = Guid.NewGuid(); // Replace with the appropriate user ID

            teamcon = await _teamConfigRepository.AddAsync(item);

            return _mapper.Map<TeamConfigResponse>(teamcon);
        }
    }
}
