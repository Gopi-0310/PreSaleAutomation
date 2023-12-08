using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.RoleQueries
{
    public sealed record GetAllRolesQuery : IQuery<List<RoleResponse>>;
    internal sealed class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<List<RoleResponse>>(roles);
        }
    }
}
