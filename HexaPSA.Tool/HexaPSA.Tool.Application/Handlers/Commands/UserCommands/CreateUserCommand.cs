using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using static CreateUserHandler;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, AllUserResponse>
{
    public sealed record CreateUserCommand(string UserName,  string FullName, Guid RoleId, string EMail) : ICommand<AllUserResponse>;
    public sealed record CreateUserRequest(string UserName,  string FullName, Guid RoleId, string EMail);


    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<AllUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
       
        var user = _mapper.Map<User>(command);

        var createdUser = await _userRepository.AddAsync(user);

        return _mapper.Map<AllUserResponse>(createdUser);
    }
}
