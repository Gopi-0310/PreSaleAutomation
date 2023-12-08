using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands
{
    public sealed record UpdateWorkStreamActivityCommand(Guid Id, string Activity, Guid RoleId, Guid WorkStreamActivityId, double Hours, int Week, string? Description, Guid? ParentId) : ICommand<WorkStreamUpdateResponse>;
    public sealed record UpdateWorkStreamActivityRequest(string Activity, Guid RoleId, Guid WorkStreamActivityId, double Hours, int Week, string Description, Guid? ParentId = null);

    public class UpdateWorkStreamActivityHandler : ICommandHandler<UpdateWorkStreamActivityCommand, WorkStreamUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkStreamActivityRepository _workStreamActivityRepository;

        public UpdateWorkStreamActivityHandler(IMapper mapper, IWorkStreamActivityRepository workStreamRepository)
        {
            _mapper = mapper;
            _workStreamActivityRepository = workStreamRepository;
        }

        public async Task<WorkStreamUpdateResponse> Handle(UpdateWorkStreamActivityCommand command, CancellationToken cancellationToken)
        {
            // Check if the WorksStream with the specified ID exists
            var existingWorksStream = await _workStreamActivityRepository.GetByIdWorkStream(command.Id);
            if (existingWorksStream == null)
            {
                // Handle the case where the WorksStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the WorksStream properties
            existingWorksStream.Activity = command.Activity;
            existingWorksStream.RoleId = command.RoleId;
            existingWorksStream.WorkStreamActivityId = command.WorkStreamActivityId;
            existingWorksStream.Hours = command.Hours;
            existingWorksStream.Week = command.Week;
            existingWorksStream.Description = command.Description;
            existingWorksStream.ParentId = command.ParentId;




            // Save the updated WorksStream to the repository*/
             await _workStreamActivityRepository.UpdateAsync(existingWorksStream);

            return _mapper.Map<WorkStreamUpdateResponse>(existingWorksStream);
        }
    }
}
