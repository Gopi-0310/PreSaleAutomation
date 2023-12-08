using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands
{
    public sealed record UpdateResourceRateCardCommand(Guid Id, Guid RoleId, decimal Rate) : ICommand<ResourceRateCardResponse>;
    public sealed record UpdateResourceRateCardRequest(Guid RoleId, decimal Rate);

    public class UpdateResourceRateCardHandler : ICommandHandler<UpdateResourceRateCardCommand, ResourceRateCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IResourceRateCardRepository _rateCardRepository;

        public UpdateResourceRateCardHandler(IMapper mapper, IResourceRateCardRepository rateCardRepository)
        {
            _mapper = mapper;
            _rateCardRepository = rateCardRepository;
        }

        public async Task<ResourceRateCardResponse> Handle(UpdateResourceRateCardCommand command, CancellationToken cancellationToken)
        {
            // Check if the RateCard with the specified ID exists
            var existingRateCard = await _rateCardRepository.GetByIdAsync(command.Id);
            if (existingRateCard == null)
            {
                // Handle the case where the RateCard with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the Rate properties
            existingRateCard.RoleId = command.RoleId;
            existingRateCard.Rate = command.Rate;
           

            // Save the updated RateCard to the repository
            await _rateCardRepository.UpdateAsync(existingRateCard);

            return _mapper.Map<ResourceRateCardResponse>(existingRateCard);
        }
    }

}
