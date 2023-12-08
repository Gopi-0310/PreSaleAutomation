using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.ProjectQuery
{
    public sealed record GetProjectQuery(Guid ProjectId) : IQuery<ProjectResponse>;
    internal sealed class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, ProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId);

            
            if (project == null)
            {
                // You can throw an exception or return an appropriate response
                //throw new NotFoundException("Project not found");
            }

            return _mapper.Map<ProjectResponse>(project);
        }
    }
}
