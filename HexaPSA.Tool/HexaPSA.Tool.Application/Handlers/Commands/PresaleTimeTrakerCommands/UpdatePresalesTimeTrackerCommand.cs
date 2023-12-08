using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Commands.PresaleTimeTrakerCommands
{
    public sealed record UpdatePresalesTimeTrackerCommand(Guid Id, Guid UserId,Guid ProjectId, string Activity, string Description, double Hours,DateTime ActivityDate) : ICommand<PresalesTimeTrackerResponse>;
    public sealed record UpdatePresalesTimeTrackerRequest(string Activity, Guid UserId, Guid ProjectId, string Description, double Hours, DateTime ActivityDate);

    public class UpdatePresalesTimeTrackerHandler : ICommandHandler<UpdatePresalesTimeTrackerCommand, PresalesTimeTrackerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPresalesTimeTrackerRepository _presalesTimeTrackerRepository;

        public UpdatePresalesTimeTrackerHandler(IMapper mapper, IPresalesTimeTrackerRepository presalesTimeTrackerRepository)
        {
            _mapper = mapper;
            _presalesTimeTrackerRepository = presalesTimeTrackerRepository;
        }

        public async Task<PresalesTimeTrackerResponse> Handle(UpdatePresalesTimeTrackerCommand command, CancellationToken cancellationToken)
        {
      
            var existingPresalesTimeTracker = await _presalesTimeTrackerRepository.GetByIdAsync(command.Id);
            if (existingPresalesTimeTracker == null)
            {

            }


            existingPresalesTimeTracker.Activity = command.Activity;
            existingPresalesTimeTracker.Description = command.Description;
            existingPresalesTimeTracker.Hours = command.Hours;
            existingPresalesTimeTracker.ActivityDate = command.ActivityDate;
            existingPresalesTimeTracker.UserId = command.UserId;
            existingPresalesTimeTracker.ProjectId = command.ProjectId;


            // Save the updated PresalesTimeTracker to the repository
            await _presalesTimeTrackerRepository.UpdateAsync(existingPresalesTimeTracker);

            return _mapper.Map<PresalesTimeTrackerResponse>(existingPresalesTimeTracker);
        }
    }

}
