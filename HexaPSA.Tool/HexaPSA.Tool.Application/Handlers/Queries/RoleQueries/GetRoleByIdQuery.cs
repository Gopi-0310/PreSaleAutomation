using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.RoleQueries
{
    public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleResponse>;
    internal sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            if (role is null)
            {
                //throw new RoleNotFoundException(request.Id);
            }

            return _mapper.Map<RoleResponse>(role);
        }
    }
}
