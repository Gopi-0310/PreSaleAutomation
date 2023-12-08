using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands
{
    public sealed record CreateResourceRateCardCommand(Guid RoleId, decimal Rate) : ICommand<ResourceRateCardResponseRoleId>;
    public class CreateResourceRateCardRequest
    {
        public Guid RoleId { get; set; }
        public decimal Rate { get; set; }
       // public Guid ProjectId { get; set; } = Guid.Empty;
    }

    public class CreateReourceRateCardHandler : ICommandHandler<CreateResourceRateCardCommand, ResourceRateCardResponseRoleId>
    {
        private readonly IMapper _mapper;
        private readonly IResourceRateCardRepository _rateCard;

        public CreateReourceRateCardHandler(IMapper mapper, IResourceRateCardRepository rateCardRepository)
        {
            _mapper = mapper;
            _rateCard = rateCardRepository;
        }

        public async Task<ResourceRateCardResponseRoleId> Handle(CreateResourceRateCardCommand command, CancellationToken cancellationToken)
        {
            ResourceRate rateCard = new();

                // Map command to entity For New Create
                var item = _mapper.Map<ResourceRate>(command);
                item.CreatedBy = Guid.NewGuid(); // Replace with the appropriate user ID

                rateCard = await _rateCard.AddAsync(item);

                return _mapper.Map<ResourceRateCardResponseRoleId>(rateCard);
            
            
        }
    }
}
