using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands
{

    public sealed record DeleteWorkStreamCommand(Guid Id) : ICommand<Unit>;
    public class DeleteWorkStreamHandler : ICommandHandler<DeleteWorkStreamCommand, Unit>
    {
        private readonly IWorkStreamRepository _workStreamRepository;

        public DeleteWorkStreamHandler(IWorkStreamRepository workStreamRepository)
        {
            _workStreamRepository = workStreamRepository;
        }

        public async Task<Unit> Handle(DeleteWorkStreamCommand command, CancellationToken cancellationToken)
        {
            // Check if the existingworkStream  with the specified ID exists
            var existingworkStream = await _workStreamRepository.GetByIdAsync(command.Id);
            if (existingworkStream == null)
            {
                // Handle the case where the existingworkStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the existingworkStream from the repository
            await _workStreamRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
