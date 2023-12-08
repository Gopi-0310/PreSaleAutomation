using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands
{
    public sealed record DeleteProjectTypeCommands(Guid Id) : ICommand<Unit>;
    public class DeleteProjectTypeCommandsHandler : ICommandHandler<DeleteProjectTypeCommands, Unit>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public DeleteProjectTypeCommandsHandler(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public async Task<Unit> Handle(DeleteProjectTypeCommands command, CancellationToken cancellationToken)
        {
            // Check if the existingworkStreamActivity  with the specified ID exists
            var existingworkStream = await _projectTypeRepository.GetByIdAsync(command.Id);
            if (existingworkStream == null)
            {
                // Handle the case where the existingworkStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the existingworkStreamActivity from the repository
            await _projectTypeRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
