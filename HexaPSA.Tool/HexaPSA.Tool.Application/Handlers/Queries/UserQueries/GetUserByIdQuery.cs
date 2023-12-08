using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<AllUserResponse>;
public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, AllUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AllUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            // Handle the case where the user is not found, e.g., throw a UserNotFoundException
        }

        return _mapper.Map<AllUserResponse>(user);
    }
}
