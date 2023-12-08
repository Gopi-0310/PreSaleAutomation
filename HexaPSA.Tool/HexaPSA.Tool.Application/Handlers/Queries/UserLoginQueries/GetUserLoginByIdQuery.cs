using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using static GetUserLoginByIdQueryHandler;


public sealed record GetUserLoginByIdQuery(Guid Id) : IQuery<AllUserLoginResponse>;
public sealed class GetUserLoginByIdQueryHandler : IQueryHandler<GetUserLoginByIdQuery, AllUserLoginResponse>
{
    private readonly IUserLoginRepository _userLoginRepository;
    private readonly IMapper _mapper;

    public GetUserLoginByIdQueryHandler(IUserLoginRepository userLoginRepository, IMapper mapper)
    {
        _userLoginRepository = userLoginRepository;
        _mapper = mapper;
    }
    public async Task<AllUserLoginResponse> Handle(global::GetUserLoginByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userLoginRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            // Handle the case where the user is not found, e.g., throw a UserNotFoundException
        }

        return _mapper.Map<AllUserLoginResponse>(user);
    }
}
