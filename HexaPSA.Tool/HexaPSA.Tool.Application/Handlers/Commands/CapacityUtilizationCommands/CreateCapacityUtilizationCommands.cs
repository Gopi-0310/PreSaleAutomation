using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands
{
    public sealed record CreateCapacityUtilizationCommand(Guid RoleId, Guid ProjectId, double Hours, decimal Rate, string Location) : ICommand<CapacityUtilizationResponse>;
    public sealed record CreateCapacityUtilizationRequest(Guid RoleId, Guid ProjectId, double Hours, decimal Rate, string Location);

    public class CreateCapacityUtilizationHandler : ICommandHandler<CreateCapacityUtilizationCommand, CapacityUtilizationResponse>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICapacityUtilizationRepository _capacityUtilization;
        private readonly IResourceRateCardRepository _resourceRate;

        public CreateCapacityUtilizationHandler(IMapper mapper, ISender sender, ICapacityUtilizationRepository capacityRepository, IResourceRateCardRepository resourceRate)
        {
            _sender = sender;
            _mapper = mapper;
            _capacityUtilization = capacityRepository;
            _resourceRate = resourceRate;
        }

        public async Task<CapacityUtilizationResponse> Handle(CreateCapacityUtilizationCommand command, CancellationToken cancellationToken)
        {
            CapacityUtilization capacity = new();
            ResourceRate resourceRate = new();
            // Map command to entity
            var item = _mapper.Map<CapacityUtilization>(command);
            item.CreatedBy = Guid.NewGuid(); // Replace with the appropriate user ID

            capacity = await _capacityUtilization.AddAsync(item);
            if (command.Rate > 0)
            {
                var createRateCardRequest = new ResourceRate
                {
                    ProjectId = command.ProjectId,
                    RoleId = command.RoleId,
                    Rate = command.Rate,
                };
                resourceRate = await _resourceRate.AddAsync(createRateCardRequest);

               // var rateCard = await _sender.Send(rateCardCommand, cancellationToken);
            }
            return _mapper.Map<CapacityUtilizationResponse>(capacity);
        }
    }
}
