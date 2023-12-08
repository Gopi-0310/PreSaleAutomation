using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands
{
    public sealed record DeleteCapacityUtilizationCommand(Guid Id) : ICommand<Unit>;
    public class DeleteCapacityUtilizationHandler : ICommandHandler<DeleteCapacityUtilizationCommand, Unit>
    {
        private readonly ICapacityUtilizationRepository _capaityRepository;

        public DeleteCapacityUtilizationHandler(ICapacityUtilizationRepository capacityRepository)
        {
            _capaityRepository = capacityRepository;
        }

        public async Task<Unit> Handle(DeleteCapacityUtilizationCommand command, CancellationToken cancellationToken)
        {
            // Check if the existingCapacityUtilization  with the specified ID exists
            var existingCapacityUtilization = await _capaityRepository.GetByIdAsync(command.Id);
            if (existingCapacityUtilization == null)
            {
                // Handle the case where the CapacityUtilization with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the CapacityUtilization from the repository
            await _capaityRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }

}
