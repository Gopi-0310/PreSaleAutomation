using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Handlers.Commands.PresaleTimeTrakerCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.ProjectCommands
{
    public sealed record DeleteProjectCommand(Guid Id) : ICommand<Unit>;

    public class DeleteProjectHandler : ICommandHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var existingProject = await _projectRepository.GetByIdAsync(command.Id);
            if (existingProject == null)
            {
            }
            await _projectRepository.DeleteAsync(command.Id);
            return Unit.Value;
        }
    }
}
