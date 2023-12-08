using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Exceptions.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Commands.RoleCommands
{
    public sealed record CreateRoleCommand(string Name, string Code) : ICommand<RoleResponse>;
    public class CreateRoleRequest { 
       public string Name { get; set; }
       public string Code { get; set; }
    };

    public class CreateRoleHandler : ICommandHandler<CreateRoleCommand, RoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public CreateRoleHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponse> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            Role role = new();

            // Check if there's already a role with the same name
            var exists = await _roleRepository.GetByNameAsync(command.Name);
            if (exists != null)
                throw new ProjectTypeExistsException(exists.Name);

            // Map command to entity
            var item = _mapper.Map<Role>(command);
            item.CreatedBy = Guid.NewGuid(); // Replace with the appropriate user ID

            role = await _roleRepository.AddAsync(item);

            return _mapper.Map<RoleResponse>(role);
        }

    }
}
