using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using static CreatePresalesTimeTrackerHandler;

public class CreatePresalesTimeTrackerHandler : ICommandHandler<CreatePresalesTimeTrackerCommand, PresalesTimeTrackerResponse>
{
    public sealed record CreatePresalesTimeTrackerCommand(Guid? UserId, string Activity, string Description, double? Hours, Guid? ProjectId, DateTime? ActivityDate) : ICommand<PresalesTimeTrackerResponse>;
    public sealed record CreatePresalesTimeTrackerRequest(Guid? UserId, string Activity, string Description, double? Hours, Guid? ProjectId, DateTime? ActivityDate);

    private readonly IMapper _mapper;
    private readonly IPresalesTimeTrackerRepository _presalesTimeTrackerRepository;

    public CreatePresalesTimeTrackerHandler(IMapper mapper, IPresalesTimeTrackerRepository presalesTimeTrackerRepository)
    {
        _mapper = mapper;
        _presalesTimeTrackerRepository = presalesTimeTrackerRepository;
    }

    public async Task<PresalesTimeTrackerResponse> Handle(CreatePresalesTimeTrackerCommand command, CancellationToken cancellationToken)
    {
        // Map command to entity
        var item = _mapper.Map<PresalesTimeTracker>(command);

        // Set any additional properties as needed
        // For example, you can set the CreatedBy property with the user's ID.

        var presalesTimeTracker = await _presalesTimeTrackerRepository.AddAsync(item);

        return _mapper.Map<PresalesTimeTrackerResponse>(presalesTimeTracker);
    }
}
