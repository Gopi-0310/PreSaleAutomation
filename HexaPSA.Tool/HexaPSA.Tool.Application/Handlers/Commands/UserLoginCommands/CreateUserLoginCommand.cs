using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using static CreateUserLoginHandler;

    public sealed record class CreateUserLoginHandler : ICommandHandler<CreateUserLoginCommand, AllUserLoginResponse>
    {
        public sealed record CreateUserLoginCommand(Guid UserId, string PasswordHash, string PasswordSalt, string EMail, bool IsVerified) : ICommand<AllUserLoginResponse>;

        public sealed record CreateUserLoginRequest(Guid UserId, string PasswordHash, string PasswordSalt, string EMail, bool IsVerified);
      
        private readonly IMapper _mapper;
        private readonly IUserLoginRepository _userLoginRepository;

        public CreateUserLoginHandler(IMapper mapper, IUserLoginRepository userLoginRepository)
        {
            _mapper = mapper;
            _userLoginRepository = userLoginRepository;
        }
        public async Task<AllUserLoginResponse> Handle(CreateUserLoginCommand command, CancellationToken cancellationToken)
        {
            var userLogin = _mapper.Map<UserLogin>(command);

            var createdUserLogin = await _userLoginRepository.AddAsync(userLogin);

            return _mapper.Map<AllUserLoginResponse>(createdUserLogin);
        }
    }
