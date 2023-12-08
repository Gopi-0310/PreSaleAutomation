using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands
{
    public sealed record DeleteResourceRateCardCommand(Guid Id) : ICommand<Unit>;
    public class DeleteResourceRateCardHandler : ICommandHandler<DeleteResourceRateCardCommand, Unit>
    {
        private readonly IResourceRateCardRepository _rateCardRepository;

        public DeleteResourceRateCardHandler(IResourceRateCardRepository rateCardRepository)
        {
            _rateCardRepository = rateCardRepository;
        }

        public async Task<Unit> Handle(DeleteResourceRateCardCommand command, CancellationToken cancellationToken)
        {
            // Check if the existingRateCard  with the specified ID exists
            var existingRateCard = await _rateCardRepository.GetByIdAsync(command.Id);
            if (existingRateCard == null)
            {
                // Handle the case where the RateCard with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the RateCard from the repository
            await _rateCardRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }

}
