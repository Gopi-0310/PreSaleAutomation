using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.TeamConfigurationCommands
{
    public sealed record DeleteTeamConfigurationsCommand(Guid Id) : ICommand<Unit>;

    public class DeleteTeamConfigurationsHandler : ICommandHandler<DeleteTeamConfigurationsCommand, Unit>
    {
        private readonly ITeamConfigurationRepository _teamConfigurationRepository;

        public DeleteTeamConfigurationsHandler(ITeamConfigurationRepository teamConfigurationRepository)
        {
            _teamConfigurationRepository = teamConfigurationRepository;
        }

        public async Task<Unit> Handle(DeleteTeamConfigurationsCommand command, CancellationToken cancellationToken)
        {
            
            var existingTeamConfig = await _teamConfigurationRepository.GetTeamConfigurationByIdAsync(command.Id);
            if (existingTeamConfig == null)
            {
        
            }

            await _teamConfigurationRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
