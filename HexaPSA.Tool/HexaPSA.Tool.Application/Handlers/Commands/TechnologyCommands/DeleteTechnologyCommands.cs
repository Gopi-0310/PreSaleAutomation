using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.TechnologyCommands
{
    public sealed record DeleteTechnologyCommands(Guid Id) : ICommand<Unit>;
    public class DeleteTechnologyCommandsHandler : ICommandHandler<DeleteTechnologyCommands, Unit>
    {
        private readonly ITechnologyRepository _technologyRepository;

        public DeleteTechnologyCommandsHandler(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task<Unit> Handle(DeleteTechnologyCommands command, CancellationToken cancellationToken)
        {
            // Check if the existingworkStreamActivity  with the specified ID exists
            var existingworkStream = await _technologyRepository.GetByIdAsync(command.Id);
            if (existingworkStream == null)
            {
                // Handle the case where the existingworkStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the existingworkStreamActivity from the repository
            await _technologyRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
