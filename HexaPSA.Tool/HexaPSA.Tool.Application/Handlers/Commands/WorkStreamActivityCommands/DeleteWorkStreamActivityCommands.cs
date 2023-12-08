using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamActivityCommands
{
    public sealed record DeleteWorkStreamActivityCommand(Guid Id) : ICommand<Unit>;
    public class DeleteWorkStreamActivityHandler : ICommandHandler<DeleteWorkStreamActivityCommand, Unit>
    {
        private readonly IWorkStreamActivityRepository _workStreamRepository;

        public DeleteWorkStreamActivityHandler(IWorkStreamActivityRepository workStreamRepository)
        {
            _workStreamRepository = workStreamRepository;
        }

        public async Task<Unit> Handle(DeleteWorkStreamActivityCommand command, CancellationToken cancellationToken)
        {
            // Check if the existingworkStreamActivity  with the specified ID exists
            var existingworkStream = await _workStreamRepository.GetByIdAsync(command.Id);
            if (existingworkStream == null)
           {
                // Handle the case where the existingworkStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the existingworkStreamActivity from the repository
            await _workStreamRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
