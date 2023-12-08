using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.RoleCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.TeamConfigurationCommands
{
    public sealed record UpdateTeamConfigurationsCommand(Guid Id, Guid UserId, Guid ProjectId, Guid RoleId) : ICommand<TeamConfigResponse>;
    public sealed record UpdateTeamConfigurationsRequest(Guid UserId, Guid ProjectId, Guid RoleId);

    public class UpdateTeamConfigurationsHandler : ICommandHandler<UpdateTeamConfigurationsCommand, TeamConfigResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamConfigurationRepository _teamConfigRepository;

        public UpdateTeamConfigurationsHandler(IMapper mapper, ITeamConfigurationRepository teamConfigRepository)
        {
            _mapper = mapper;
            _teamConfigRepository = teamConfigRepository;
        }

        public async Task<TeamConfigResponse> Handle(UpdateTeamConfigurationsCommand command, CancellationToken cancellationToken)
        {
            var existingTeam = await _teamConfigRepository.GetByIdAsync(command.Id);
            if (existingTeam == null)
            {

            }

            // Update the role properties
            existingTeam.UserId = command.UserId;
            existingTeam.ProjectId = command.ProjectId;
            existingTeam.RoleId = command.RoleId;

            // Save the updated role to the repository
            await _teamConfigRepository.UpdateAsync(existingTeam);
            return _mapper.Map<TeamConfigResponse>(existingTeam);
        }
    }
}