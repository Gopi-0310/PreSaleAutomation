using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Commands.RoleCommands
{
    public sealed record UpdateRoleCommand(Guid Id, string Name, string Code) : ICommand<RoleResponse>;
    public sealed record UpdateRoleRequest(string Name, string Code);

    public class UpdateRoleHandler : ICommandHandler<UpdateRoleCommand, RoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponse> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            // Check if the role with the specified ID exists
            var existingRole = await _roleRepository.GetByIdAsync(command.Id);
            if (existingRole == null)
            {

            }

            // Update the role properties
            existingRole.Name = command.Name;
            existingRole.Code = command.Code;

            // Save the updated role to the repository
            await _roleRepository.UpdateAsync(existingRole);

            return _mapper.Map<RoleResponse>(existingRole);
        }
    }

}
