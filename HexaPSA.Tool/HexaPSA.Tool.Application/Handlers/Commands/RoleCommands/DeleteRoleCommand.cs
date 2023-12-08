using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;

namespace HexaPSA.Tool.Application.Handlers.Commands.RoleCommands
{
    public sealed record DeleteRoleCommand(Guid Id) : ICommand<Unit>;

    public class DeleteRoleHandler : ICommandHandler<DeleteRoleCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            // Check if the role with the specified ID exists
            var existingRole = await _roleRepository.GetByIdAsync(command.Id);
            if (existingRole == null)
            {
                // Handle the case where the role with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Delete the role from the repository
            await _roleRepository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }

}
