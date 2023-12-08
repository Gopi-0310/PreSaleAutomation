using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Exceptions;

namespace HexaPSA.Tool.Application.Handlers.Queries.ProjectQueries
{
    public sealed record GetProjectTypeByIdQuery(Guid Id) : IQuery<ProjectTypeResponse>;


    internal sealed class GetProjectTypeByIdQueryHandler : IQueryHandler<GetProjectTypeByIdQuery, ProjectTypeResponse>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IMapper _mapper;

        public GetProjectTypeByIdQueryHandler(IMapper mapper, IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTypeResponse> Handle(GetProjectTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var projectType = await _projectTypeRepository.GetByIdAsync(request.Id);

            if (projectType is null)
            {
                throw new ProjectTypeNotFoundException(request.Id);
            }

            return _mapper.Map<ProjectTypeResponse>(projectType);
        }
    }

}
