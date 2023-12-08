using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.PresaleTimeTrakerCommands
{
    public sealed record DeletePresalesTimeTrackerCommand(Guid Id) : ICommand<Unit>;
    public class DeletePresalesTimeTrackerHandler : ICommandHandler<DeletePresalesTimeTrackerCommand, Unit>
    {
        private readonly IPresalesTimeTrackerRepository _presalesTimeTrackerRepository;

        public DeletePresalesTimeTrackerHandler(IPresalesTimeTrackerRepository presalesTimeTrackerRepository)
        {
            _presalesTimeTrackerRepository = presalesTimeTrackerRepository;
        }

        public async Task<Unit> Handle(DeletePresalesTimeTrackerCommand command, CancellationToken cancellationToken)
        {
            
            var existingPresalesTimeTracker = await _presalesTimeTrackerRepository.GetByIdAsync(command.Id);
            if (existingPresalesTimeTracker == null)
            {
        
            }

         
            await _presalesTimeTrackerRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }

}
