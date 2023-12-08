using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands
{
    public sealed record UpdateWorkStreamCommands(Guid Id,string Name,Guid ProjectId, int Orders) : ICommand<WorkStream>;
    public sealed record UpdateWorkStreamRequest(string Name, Guid ProjectId, int Orders);

    public class UpdateWorkStreamHandler : ICommandHandler<UpdateWorkStreamCommands, WorkStream>
    {
        private readonly IMapper _mapper;
        private readonly IWorkStreamRepository _workStreamRepository;

        public UpdateWorkStreamHandler(IMapper mapper, IWorkStreamRepository workStreamRepository)
        {
            _mapper = mapper;
            _workStreamRepository = workStreamRepository;
        }

        public async Task<WorkStream> Handle(UpdateWorkStreamCommands command, CancellationToken cancellationToken)
        {
            // Check if the WorksStream with the specified ID exists
            var existingWorksStream = await _workStreamRepository.GetByIdDataAsync(command.Id);
            if (existingWorksStream == null)
            {
                // Handle the case where the WorksStream with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the WorksStream properties
            existingWorksStream.Name = command.Name;

            existingWorksStream.ProjectId = command.ProjectId;

            existingWorksStream.Orders = command.Orders;
            var mappindData = _mapper.Map<WorkStream>(existingWorksStream);

            // Save the updated WorksStream to the repository
            await _workStreamRepository.UpdateAsync(mappindData);

            return _mapper.Map<WorkStream>(existingWorksStream);
        }
    }
}
