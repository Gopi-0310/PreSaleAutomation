using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GetAllProjectQueryHandler;

public sealed class GetAllProjectQueryHandler : IQueryHandler<GetAllProjectQuery, List<ProjectResponseAll>>
{
    public sealed record GetAllProjectQuery : IQuery<List<ProjectResponseAll>>;

    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetAllProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<ProjectResponseAll>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetAllAsync();

        return _mapper.Map<List<ProjectResponseAll>>(project);
    }
}
