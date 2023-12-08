using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands
{
    public sealed record CreateWorkStreamCommands(string Name,Guid ProjectId) : ICommand<WorkStreamResponse>;
    public sealed record CreateWorkStreamRequest(string Name, Guid ProjectId);

    public class CreateWorkSreamHandler : ICommandHandler<CreateWorkStreamCommands, WorkStreamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkStreamRepository _WorkSream;

        public CreateWorkSreamHandler(IMapper mapper, IWorkStreamRepository workStream)
        {
            _mapper = mapper;
            _WorkSream = workStream;
        }

        public async Task<WorkStreamResponse> Handle(CreateWorkStreamCommands command, CancellationToken cancellationToken)
        {
            WorkStream workStream = new();

            // Map command to entity For New Create
            var item = _mapper.Map<WorkStream>(command);
            item.CreatedBy = Guid.NewGuid(); 

            workStream = await _WorkSream.AddAsync(item);

            return _mapper.Map<WorkStreamResponse>(workStream);


        }
    }
}
