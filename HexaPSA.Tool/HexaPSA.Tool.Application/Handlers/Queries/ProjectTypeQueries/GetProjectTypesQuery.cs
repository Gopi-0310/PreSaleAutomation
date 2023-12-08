
using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;


namespace HexaPSA.Tool.Application.Handlers.Queries.ProjectQueries
{
    public sealed record GetProjectTypesQuery() : IQuery<List<ProjectTypeResponse>>;


    internal sealed class GetProjectTypesQueryHandler : IQueryHandler<GetProjectTypesQuery, List<ProjectTypeResponse>>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IMapper _mapper;

        public GetProjectTypesQueryHandler(IMapper mapper, IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectTypeResponse>> Handle(GetProjectTypesQuery request, CancellationToken cancellationToken)
        {
            var projectTypes = await _projectTypeRepository.GetAllAsync();

            return _mapper.Map<List<ProjectTypeResponse>>(projectTypes);
        }
    }

}


