using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationCommands
{
    public sealed record UpdateCapacityUtilizationCommand(Guid Id, Guid RoleId,Guid ResourceId, Guid ProjectId, double Hours, decimal Rate, string Location) : ICommand<CapacityUtilizationResponse>;
    public sealed record UpdateCapacityUtilizationRequest(Guid RoleId, Guid ResourceId ,Guid ProjectId, double Hours, decimal Rate, string Location);

    public class UpdateResourceRateCardHandler : ICommandHandler<UpdateCapacityUtilizationCommand, CapacityUtilizationResponse>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICapacityUtilizationRepository _capacityRepository;
        private readonly IResourceRateCardRepository _rateCardRepository;
        public UpdateResourceRateCardHandler(IMapper mapper, ISender sender, ICapacityUtilizationRepository capacityRepository, IResourceRateCardRepository ratecard)
        {
            _sender = sender;
            _mapper = mapper;
            _capacityRepository = capacityRepository;
            _rateCardRepository = ratecard;
        }

        public async Task<CapacityUtilizationResponse> Handle(UpdateCapacityUtilizationCommand command, CancellationToken cancellationToken)
        {
            // Check if the CapacityUtilization with the specified ID exists
            var existingCapacityUtilization = await _capacityRepository.GetByIdAsync(command.Id);
            if (existingCapacityUtilization == null)
            {
                // Handle the case where the Capacity with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the Capacity properties
            existingCapacityUtilization.RoleId = command.RoleId;

            existingCapacityUtilization.ProjectId = command.ProjectId;

            existingCapacityUtilization.Hours = command.Hours;
            existingCapacityUtilization.Location = command.Location;

            //Update the rate card Rate
            var updateRateCardRequest = new ResourceRate
            {
                Id = command.ResourceId,
                ProjectId = command.ProjectId,
                RoleId = command.RoleId,
                Rate = command.Rate,
            };
            await _rateCardRepository.UpdateAsync(updateRateCardRequest);


            // Save the updated Capacity to the repository
            await _capacityRepository.UpdateAsync(existingCapacityUtilization);

            return _mapper.Map<CapacityUtilizationResponse>(existingCapacityUtilization);
        }
    }
}
